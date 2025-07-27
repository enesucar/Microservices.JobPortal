using AutoMapper;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.Guids;
using CareerWay.Shared.Security.Users;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobSeekerService.Application.Services;

public class JobSeekerSkillService : IJobSeekerSkillService
{
    private readonly IJobSeekerDbContext _jobSeekerDbContext;
    private readonly IMapper _mapper;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IUser _user;

    public JobSeekerSkillService(
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

    public async Task Create(CreateJobSeekerSkillRequest request)
    {
        var jobSeekerSkill = _mapper.Map<JobSeekerSkill>(request);
        jobSeekerSkill.Id = _guidGenerator.Generate();
        jobSeekerSkill.JobSeekerId = _user.Id;

        await _jobSeekerDbContext.JobSeekerSkills.AddAsync(jobSeekerSkill);
        await _jobSeekerDbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var jobSeekerSkill = await _jobSeekerDbContext.JobSeekerSkills.FirstOrDefaultAsync(o => o.Id == id);
        if (jobSeekerSkill == null)
        {
            throw new NotFoundException();
        }

        _jobSeekerDbContext.JobSeekerSkills.Remove(jobSeekerSkill);
        await _jobSeekerDbContext.SaveChangesAsync();
    }
}
