using AutoMapper;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Domain.Entities;

namespace CareerWay.JobSeekerService.Application.Mappings;

public class JobSeekerSkillProfile : Profile
{
    public JobSeekerSkillProfile()
    {
        CreateMap<JobSeekerSkill, CreateJobSeekerSkillRequest>().ReverseMap();
    }
}
