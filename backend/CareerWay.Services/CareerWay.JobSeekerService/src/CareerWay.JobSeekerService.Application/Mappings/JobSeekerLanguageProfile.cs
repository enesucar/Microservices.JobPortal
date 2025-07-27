using AutoMapper;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Domain.Entities;

namespace CareerWay.JobSeekerService.Application.Mappings;

public class JobSeekerLanguageProfile : Profile
{
    public JobSeekerLanguageProfile()
    {
        CreateMap<JobSeekerLanguage, CreateJobSeekerLanguageRequest>().ReverseMap();
    }
}
