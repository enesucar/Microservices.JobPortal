using CareerWay.JobSeekerService.Application.Interfaces;
using Grpc.Core;
using JobSeekerGrpcService;

namespace CareerWay.JobSeekerService.API.Services;

public class JobSeekerGrpcServiceImplementation : JobSeekerGrpcService.JobSeekerService.JobSeekerServiceBase
{
    private readonly IJobSeekerGrpcClient _jobSeekerGrpcClient;

    public JobSeekerGrpcServiceImplementation(IJobSeekerGrpcClient jobSeekerGrpcClient)
    {
        _jobSeekerGrpcClient = jobSeekerGrpcClient;
    }

    public override async Task<JobSeekerListResponse> GetJobSeekerList(JobSeekerListRequest request, ServerCallContext context)
    {
        var jobSeekers = await _jobSeekerGrpcClient.GetByIdAsync(request.Ids.ToList());
        var jobSeekerListResponseModel = jobSeekers.Select(jobSeeker => new JobSeekerListResponseModel
        {
            Id = jobSeeker.Id,
            FirstName = jobSeeker.FirstName,
            LastName = jobSeeker.LastName,
            ProfilePhoto = jobSeeker.ProfilePhoto
        }).ToList();

        var jobSeekerListResponse = new JobSeekerListResponse();
        jobSeekerListResponse.JobSeekers.AddRange(jobSeekerListResponseModel);

        return jobSeekerListResponse;
    }
}
