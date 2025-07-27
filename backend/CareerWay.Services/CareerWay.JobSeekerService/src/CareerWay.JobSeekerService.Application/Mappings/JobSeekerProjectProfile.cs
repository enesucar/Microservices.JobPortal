using AutoMapper;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Domain.Entities;

namespace CareerWay.JobSeekerService.Application.Mappings;

public class JobSeekerProjectProfile : Profile
{
    public JobSeekerProjectProfile()
    {
        CreateMap<JobSeekerProject, CreateJobSeekerProjectRequest>().ReverseMap();
    }
}
