using AutoMapper;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Domain.Entities;

namespace CareerWay.JobSeekerService.Application.Mappings;

public class JobSeekerSertificateProfile : Profile
{
    public JobSeekerSertificateProfile()
    {
        CreateMap<JobSeekerCertificate, CreateJobSeekerCertificateRequest>().ReverseMap();
    }
}
