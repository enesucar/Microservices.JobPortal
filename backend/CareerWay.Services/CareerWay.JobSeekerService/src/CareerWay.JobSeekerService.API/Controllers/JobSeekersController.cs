using Asp.Versioning;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.Shared.AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.JobSeekerService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class JobSeekersController : ControllerBase
{
    private readonly IJobSeekerService _jobSeekerService;

    public JobSeekersController(IJobSeekerService jobSeekerService)
    {
        _jobSeekerService = jobSeekerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] JobSeekersRequest request)
    {
        var jobSeeker = await _jobSeekerService.GetList(request);
        return Ok(jobSeeker);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail([FromRoute] long id)
    {
        var jobSeeker = await _jobSeekerService.GetDetail(id);
        return Ok(jobSeeker);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJobSeekerRequest request)
    {
        await _jobSeekerService.Create(request);
        return Created("", new SuccessApiResponse());
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] EditJobSeekerRequest request)
    {
        await _jobSeekerService.Edit(request);
        return Ok(new SuccessApiResponse());
    }
}
