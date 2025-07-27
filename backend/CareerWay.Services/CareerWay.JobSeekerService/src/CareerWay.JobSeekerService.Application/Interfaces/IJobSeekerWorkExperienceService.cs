using CareerWay.JobSeekerService.Application.Models;

namespace CareerWay.JobSeekerService.Application.Interfaces;

public interface IJobSeekerWorkExperienceService
{
    Task Create(CreateJobSeekerWorkExperienceRequest request);

    Task Delete(Guid id);
}
