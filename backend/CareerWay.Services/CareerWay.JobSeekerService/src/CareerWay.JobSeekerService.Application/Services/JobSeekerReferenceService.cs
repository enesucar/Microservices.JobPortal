using AutoMapper;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.Guids;
using CareerWay.Shared.Security.Users;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobSeekerService.Application.Services;

public class JobSeekerReferenceService : IJobSeekerReferenceService
{
    private readonly IJobSeekerDbContext _jobSeekerDbContext;
    private readonly IMapper _mapper;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IUser _user;

    public JobSeekerReferenceService(
        IJobSeekerDbContext jobSeekerDbContext,
        IMapper mapper,
        IGuidGenerator guidGenerator,
        IUser user)
    {
        _jobSeekerDbContext = jobSeekerDbContext;
        _mapper = mapper;
        _guidGenerator = guidGenerator;
        _user = user;
    }

    public async Task Create(List<CreateJobSeekerReferenceRequest> request)
    {   
        await _jobSeekerDbContext.JobSeekerReferences.Where(o => o.JobSeekerId == _user.Id).ExecuteDeleteAsync();

        var jobSeekerReferences = _mapper.Map<List<JobSeekerReference>>(request);
        foreach (var jobSeekerReference in jobSeekerReferences)
        {
            jobSeekerReference.Id = _guidGenerator.Generate();
            jobSeekerReference.JobSeekerId = _user.Id;
        }

        await _jobSeekerDbContext.JobSeekerReferences.AddRangeAsync(jobSeekerReferences);
        await _jobSeekerDbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var jobSeekerReference = await _jobSeekerDbContext.JobSeekerReferences.FirstOrDefaultAsync(o => o.Id == id);
        if (jobSeekerReference == null)
        {
            throw new NotFoundException();
        }

        _jobSeekerDbContext.JobSeekerReferences.Remove(jobSeekerReference);
        await _jobSeekerDbContext.SaveChangesAsync();
    }
}
