using CareerWay.JobSeekerService.Application.Models;

namespace CareerWay.JobSeekerService.Application.Interfaces;

public interface IJobSeekerService
{
    Task<JobSeekersResponse> GetList(JobSeekersRequest request);

    Task<JobSeekerDetailResponse> GetDetail(long id);

    Task Create(CreateJobSeekerRequest request);

    Task Edit(EditJobSeekerRequest request);
}
