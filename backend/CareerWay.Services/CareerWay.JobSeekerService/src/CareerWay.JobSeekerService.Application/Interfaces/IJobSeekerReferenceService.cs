using CareerWay.JobSeekerService.Application.Models;

namespace CareerWay.JobSeekerService.Application.Interfaces;

public interface IJobSeekerReferenceService
{
    Task Create(List<CreateJobSeekerReferenceRequest> request);

    Task Delete(Guid id);
}
