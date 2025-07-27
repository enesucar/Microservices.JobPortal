using AutoMapper;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Domain.Entities;

namespace CareerWay.JobSeekerService.Application.Mappings;

public class JobSeekerSchoolProfile : Profile
{
    public JobSeekerSchoolProfile()
    {
        CreateMap<JobSeekerSchool, CreateJobSeekerSchoolRequest>().ReverseMap();
    }
}
