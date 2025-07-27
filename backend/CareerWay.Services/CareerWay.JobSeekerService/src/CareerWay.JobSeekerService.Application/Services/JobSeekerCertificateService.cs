using AutoMapper;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.Guids;
using CareerWay.Shared.Security.Users;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobSeekerService.Application.Services;

public class JobSeekerCertificateService : IJobSeekerCertificateService
{
    private readonly IJobSeekerDbContext _jobSeekerDbContext;
    private readonly IMapper _mapper;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IUser _user;

    public JobSeekerCertificateService(
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

    public async Task Create(CreateJobSeekerCertificateRequest request)
    {
        var jobSeekerCertificate = _mapper.Map<JobSeekerCertificate>(request);
        jobSeekerCertificate.Id = _guidGenerator.Generate();
        jobSeekerCertificate.JobSeekerId = _user.Id;

        await _jobSeekerDbContext.JobSeekerCertificates.AddAsync(jobSeekerCertificate);
        await _jobSeekerDbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var jobSeekerCertificate = await _jobSeekerDbContext.JobSeekerCertificates.FirstOrDefaultAsync(o => o.Id == id);
        if (jobSeekerCertificate == null)
        {
            throw new NotFoundException();
        }

        _jobSeekerDbContext.JobSeekerCertificates.Remove(jobSeekerCertificate);
        await _jobSeekerDbContext.SaveChangesAsync();
    }
}
