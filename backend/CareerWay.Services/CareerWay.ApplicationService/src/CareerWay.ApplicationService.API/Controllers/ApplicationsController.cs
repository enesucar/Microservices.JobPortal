using Asp.Versioning;
using CareerWay.ApplicationService.API.Data.Contexts;
using CareerWay.ApplicationService.API.Entities;
using CareerWay.ApplicationService.API.IntegrationEvents.Events;
using CareerWay.ApplicationService.API.Interfaces;
using CareerWay.ApplicationService.API.Models;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.EventBus.Events;
using CareerWay.Shared.Guids;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.ApplicationService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class ApplicationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IUser _user;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IEventBus _eventBus;
    private readonly IJobSeekerGrpcClient _jobSeekerGrpcClient;

    public ApplicationsController(
        ApplicationDbContext context,
        IUser user,
        IGuidGenerator guidGenerator,
        IEventBus eventBus,
        IJobSeekerGrpcClient jobSeekerGrpcClient)
    {
        _context = context;
        _user = user;
        _guidGenerator = guidGenerator;
        _eventBus = eventBus;
        _jobSeekerGrpcClient = jobSeekerGrpcClient;
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetList([FromRoute] long postId)
    {
        var response = new ApplicantionsResponse();
        var applications = await _context.Applications.Where(o => o.JobAdvertId == postId).ToListAsync();
        if (applications.Count == 0)
        {
            return Ok(response);
        }

        var jobSeekerIds = applications.Select(o => o.JobSeekerId).ToList();
        var jobSeekers = await _jobSeekerGrpcClient.GetListByIds(jobSeekerIds);

        foreach (var application in applications)
        {
            var jobSeeker = jobSeekers.FirstOrDefault(o => o.Id == application.JobSeekerId);
            if (jobSeeker != null)
            {
                response.JobSeekers.Add(new ApplicantionsJobSeekerResponse()
                {
                    Id = application.JobSeekerId,
                    FirstName = jobSeeker.FirstName,
                    LastName = jobSeeker.LastName,
                    Status = application.Status,
                    ProfilePhoto = jobSeeker.ProfilePhoto,
                    CreationDate = application.CreationDate
                });
            }
        }
        return Ok(response);
    }

    [HttpGet("{postId}/application-count")]
    public async Task<IActionResult> GetCount([FromRoute] long postId)
    {
        var count = await _context.Applications.CountAsync(o => o.JobAdvertId == postId);
        return Ok(count);
    }

    [HttpPost("{id}/check-apply")]
    public async Task<IActionResult> CheckApply([FromRoute] long id)
    {
        var isApplied = await _context.Applications.AnyAsync(o => o.JobAdvertId == id && o.JobSeekerId == _user.Id);
        return Ok(isApplied);
    }

    [HttpPost("apply")]
    public async Task<IActionResult> Apply([FromBody] Application application)
    {
        application.Id = _guidGenerator.Generate();
        await _context.Applications.AddAsync(application);
        await _context.SaveChangesAsync();

        var postAppliedIntegrationEvent = _eventBus.PublishAsync(new PostAppliedIntegrationEvent(_guidGenerator.Generate(), DateTime.Now)
        {
            Id = application.Id,
            PostId = application.JobAdvertId,
            UserId = application.JobSeekerId
        });

        return Created("", null);
    }

    [HttpPost("{id}/withdraw")]
    public async Task<IActionResult> Withdraw([FromRoute] long id)
    {
        var application = await _context.Applications.FirstOrDefaultAsync(o => o.JobAdvertId == id && o.JobSeekerId == _user.Id);
        if (application == null)
        {
            return NotFound();
        }

        _context.Applications.Remove(application);
        await _context.SaveChangesAsync();

        await _eventBus.PublishAsync(new PostWithdrawnIntegrationEvent(_guidGenerator.Generate(), DateTime.Now)
        {
            Id = application.Id
        });

        return Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> SetStatus([FromBody] Application model)
    {
        var application = await _context.Applications.FirstOrDefaultAsync(o => o.Id == model.Id);
        if (application == null)
        {
            return NotFound();
        }

        application.Status = model.Status;
        await _context.SaveChangesAsync();
        return Ok();
    }
}
