using AutoMapper;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Domain.Entities;

namespace CareerWay.JobSeekerService.Application.Mappings;

public class JobSeekerWorkExperienceProfile : Profile
{
    public JobSeekerWorkExperienceProfile()
    {
        CreateMap<JobSeekerWorkExperience, CreateJobSeekerWorkExperienceRequest>().ReverseMap();
    }
}

