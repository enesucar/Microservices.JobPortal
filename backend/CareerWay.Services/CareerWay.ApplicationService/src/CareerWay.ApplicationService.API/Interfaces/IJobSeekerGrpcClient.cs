using CareerWay.ApplicationService.API.Models;

namespace CareerWay.ApplicationService.API.Interfaces;

public interface IJobSeekerGrpcClient
{
    Task<List<JobSeeker>> GetListByIds(List<long> ids);
}
