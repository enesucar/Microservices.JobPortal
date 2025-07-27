using CareerWay.ApplicationService.API.Interfaces;
using CareerWay.ApplicationService.API.Models;
using JobSeekerGrpcService;

namespace CareerWay.ApplicationService.API.Services;

public class JobSeekerGrpcClient : IJobSeekerGrpcClient
{
    private readonly JobSeekerService.JobSeekerServiceClient _jobSeekerClient;

    public JobSeekerGrpcClient(JobSeekerService.JobSeekerServiceClient jobSeekerClient)
    {
        _jobSeekerClient = jobSeekerClient;
    }

    public async Task<List<JobSeeker>> GetListByIds(List<long> ids)
    {
        var jobSeekerListRequest = new JobSeekerListRequest();
        jobSeekerListRequest.Ids.AddRange(ids);
        var jobSeekers = await _jobSeekerClient.GetJobSeekerListAsync(jobSeekerListRequest);
        return jobSeekers.JobSeekers.Select(o => new JobSeeker()
        {
            Id = o.Id,
            FirstName = o.FirstName,
            LastName = o.LastName,
            ProfilePhoto = o.ProfilePhoto
        }).ToList();
    }
}
