using CareerWay.JobSeekerService.Application.Models;

namespace CareerWay.JobSeekerService.Application.Interfaces;

public interface IJobSeekerLanguageService
{
    Task Create(CreateJobSeekerLanguageRequest request);

    Task Delete(Guid id);
}
