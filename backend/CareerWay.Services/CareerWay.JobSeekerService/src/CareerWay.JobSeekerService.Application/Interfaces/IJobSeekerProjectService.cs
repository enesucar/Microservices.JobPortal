using CareerWay.JobSeekerService.Application.Models;

namespace CareerWay.JobSeekerService.Application.Interfaces;

public interface IJobSeekerProjectService
{
    Task Create(CreateJobSeekerProjectRequest request);

    Task Delete(Guid id);
}
