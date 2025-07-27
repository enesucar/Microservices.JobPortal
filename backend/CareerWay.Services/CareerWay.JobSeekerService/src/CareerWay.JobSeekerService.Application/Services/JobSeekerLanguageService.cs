using AutoMapper;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.Guids;
using CareerWay.Shared.Security.Users;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobSeekerService.Application.Services;

public class JobSeekerLanguageService : IJobSeekerLanguageService
{
    private readonly IJobSeekerDbContext _jobSeekerDbContext;
    private readonly IMapper _mapper;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IUser _user;

    public JobSeekerLanguageService(
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

    public async Task Create(CreateJobSeekerLanguageRequest request)
    {
        var jobSeekerLanguage = _mapper.Map<JobSeekerLanguage>(request);
        jobSeekerLanguage.Id = _guidGenerator.Generate();
        jobSeekerLanguage.JobSeekerId = _user.Id;

        await _jobSeekerDbContext.JobSeekerLanguages.AddAsync(jobSeekerLanguage);
        await _jobSeekerDbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var jobSeekerLanguage = await _jobSeekerDbContext.JobSeekerLanguages.FirstOrDefaultAsync(o => o.Id == id);
        if (jobSeekerLanguage == null)
        {
            throw new NotFoundException();
        }

        _jobSeekerDbContext.JobSeekerLanguages.Remove(jobSeekerLanguage);
        await _jobSeekerDbContext.SaveChangesAsync();
    }
}
