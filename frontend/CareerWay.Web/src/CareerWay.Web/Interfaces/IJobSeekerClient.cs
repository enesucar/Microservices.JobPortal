using CareerWay.Web.Models.JobSeekers;

namespace CareerWay.Web.Interfaces;

public interface IJobSeekerClient
{
    Task CreateSchools(List<CreateJobSeekerSchoolRequest> request);

    Task CreateReferences(List<CreateJobSeekerReferenceRequest> request);

    Task Edit(EditJobSeekerRequest request);

    Task<JobSeekerDetail> GetDetail(long id);
}
