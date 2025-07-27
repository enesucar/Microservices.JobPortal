using CareerWay.IdentityService.Application.Models;

namespace CareerWay.IdentityService.Application.Interfaces;

public interface IJobSeekerClient
{
    Task Create(CreateJobSeekerHttpRequest request);
}
