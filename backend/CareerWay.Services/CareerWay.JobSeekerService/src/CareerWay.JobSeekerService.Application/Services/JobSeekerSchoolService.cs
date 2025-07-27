using AutoMapper;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.Guids;
using CareerWay.Shared.Security.Users;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobSeekerService.Application.Services;

public class JobSeekerSchoolService : IJobSeekerSchoolService
{
    private readonly IJobSeekerDbContext _jobSeekerDbContext;
    private readonly IMapper _mapper;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IUser _user;

    public JobSeekerSchoolService(
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

    public async Task Create(List<CreateJobSeekerSchoolRequest> request)
    {
        await _jobSeekerDbContext.JobSeekerSchools.Where(o => o.JobSeekerId == _user.Id).ExecuteDeleteAsync();

        var jobSeekerSchools = _mapper.Map<List<JobSeekerSchool>>(request);
        foreach (var jobSeekerSchool in jobSeekerSchools)
        {
            jobSeekerSchool.Id = _guidGenerator.Generate();
            jobSeekerSchool.JobSeekerId = _user.Id;
        }

        await _jobSeekerDbContext.JobSeekerSchools.AddRangeAsync(jobSeekerSchools);
        await _jobSeekerDbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var jobSeekerSchool = await _jobSeekerDbContext.JobSeekerSchools.FirstOrDefaultAsync(o => o.Id == id);
        if (jobSeekerSchool == null)
        {
            throw new NotFoundException();
        }

        _jobSeekerDbContext.JobSeekerSchools.Remove(jobSeekerSchool);
        await _jobSeekerDbContext.SaveChangesAsync();
    }
}
