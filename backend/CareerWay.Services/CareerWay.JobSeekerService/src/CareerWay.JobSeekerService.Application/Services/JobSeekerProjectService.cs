using AutoMapper;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.Guids;
using CareerWay.Shared.Security.Users;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobSeekerService.Application.Services;

public class JobSeekerProjectService : IJobSeekerProjectService
{
    private readonly IJobSeekerDbContext _jobSeekerDbContext;
    private readonly IMapper _mapper;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IUser _user;

    public JobSeekerProjectService(
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

    public async Task Create(CreateJobSeekerProjectRequest request)
    {
        var jobSeekerProject = _mapper.Map<JobSeekerProject>(request);
        jobSeekerProject.Id = _guidGenerator.Generate();
        jobSeekerProject.JobSeekerId = _user.Id;

        await _jobSeekerDbContext.JobSeekerProjects.AddAsync(jobSeekerProject);
        await _jobSeekerDbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var jobSeekerProject = await _jobSeekerDbContext.JobSeekerProjects.FirstOrDefaultAsync(o => o.Id == id);
        if (jobSeekerProject == null)
        {
            throw new NotFoundException();
        }

        _jobSeekerDbContext.JobSeekerProjects.Remove(jobSeekerProject);
        await _jobSeekerDbContext.SaveChangesAsync();
    }
}
