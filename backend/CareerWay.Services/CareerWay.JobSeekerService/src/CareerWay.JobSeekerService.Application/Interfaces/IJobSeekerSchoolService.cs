using CareerWay.JobSeekerService.Application.Models;

namespace CareerWay.JobSeekerService.Application.Interfaces;

public interface IJobSeekerSchoolService
{
    Task Create(List<CreateJobSeekerSchoolRequest> request);

    Task Delete(Guid id);
}
