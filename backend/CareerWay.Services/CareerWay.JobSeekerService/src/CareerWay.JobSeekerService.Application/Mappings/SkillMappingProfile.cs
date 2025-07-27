using AutoMapper;
using CareerWay.JobSeekerService.Application.IntegrationEvents.Events;
using CareerWay.JobSeekerService.Domain.Entities;

namespace CareerWay.JobSeekerService.Application.Mappings;

public class SkillMappingProfile : Profile
{
    public SkillMappingProfile()
    {
        CreateMap<Skill, SkillCreatedIntegrationEvent>().ReverseMap();
        CreateMap<Skill, SkillEditedIntegrationEvent>().ReverseMap();
    }
}
