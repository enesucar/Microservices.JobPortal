using CareerWay.JobSeekerService.Application.Models;

namespace CareerWay.JobSeekerService.Application.Interfaces;

public interface IJobSeekerCertificateService
{
    Task Create(CreateJobSeekerCertificateRequest request);

    Task Delete(Guid id);
}
