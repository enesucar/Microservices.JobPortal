using CareerWay.JobSeekerService.Domain.Entities;

namespace CareerWay.JobSeekerService.Application.Interfaces;

public interface IJobSeekerGrpcClient
{
    Task<List<JobSeeker>> GetByIdAsync(List<long> ids);
}
