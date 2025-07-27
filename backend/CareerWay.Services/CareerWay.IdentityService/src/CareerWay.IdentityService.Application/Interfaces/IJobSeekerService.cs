using CareerWay.IdentityService.Application.Models;

namespace CareerWay.IdentityService.Application.Interfaces;

public interface IJobSeekerService
{
    Task Create(CreateJobSeekerRequest request);
}
