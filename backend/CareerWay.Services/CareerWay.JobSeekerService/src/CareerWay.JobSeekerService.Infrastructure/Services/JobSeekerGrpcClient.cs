using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobSeekerService.Infrastructure.Services;

public class JobSeekerGrpcClient : IJobSeekerGrpcClient
{
    private readonly IJobSeekerDbContext _jobSeekerDbContext;

    public JobSeekerGrpcClient(IJobSeekerDbContext jobSeekerDbContext)
    {
        _jobSeekerDbContext = jobSeekerDbContext;
    }

    public async Task<List<JobSeeker>> GetByIdAsync(List<long> ids)
    {
        return await _jobSeekerDbContext.JobSeekers
            .Where(o => ids.Contains(o.Id) && o.IsDeleted == 0)
            .Select(o => new JobSeeker()
            {
                Id = o.Id,
                FirstName = o.FirstName,
                LastName = o.LastName,
                ProfilePhoto = o.ProfilePhoto,
            }).ToListAsync();
    }
}
