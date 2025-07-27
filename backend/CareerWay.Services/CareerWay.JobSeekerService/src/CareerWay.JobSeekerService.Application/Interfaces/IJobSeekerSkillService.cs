using CareerWay.JobSeekerService.Application.Models;

namespace CareerWay.JobSeekerService.Application.Interfaces;

public interface IJobSeekerSkillService
{
    Task Create(CreateJobSeekerSkillRequest request);

    Task Delete(Guid id);
}
