using AutoMapper;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.Guids;
using CareerWay.Shared.Security.Users;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobSeekerService.Application.Services;

public class JobSeekerWorkExperienceService : IJobSeekerWorkExperienceService
{
    private readonly IJobSeekerDbContext _jobSeekerDbContext;
    private readonly IMapper _mapper;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IUser _user;

    public JobSeekerWorkExperienceService(
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

    public async Task Create(CreateJobSeekerWorkExperienceRequest request)
    {
        var jobSeekerWorkExperience = _mapper.Map<JobSeekerWorkExperience>(request);
        jobSeekerWorkExperience.Id = _guidGenerator.Generate();
        jobSeekerWorkExperience.JobSeekerId = _user.Id;

        await _jobSeekerDbContext.JobSeekerWorkExperiences.AddAsync(jobSeekerWorkExperience);
        await _jobSeekerDbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var jobSeekerWorkExperience = await _jobSeekerDbContext.JobSeekerWorkExperiences.FirstOrDefaultAsync(o => o.Id == id);
        if (jobSeekerWorkExperience == null)
        {
            throw new NotFoundException();
        }

        _jobSeekerDbContext.JobSeekerWorkExperiences.Remove(jobSeekerWorkExperience);
        await _jobSeekerDbContext.SaveChangesAsync();
    }
}
