using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Skills.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.Skills.Commands.Edit;
using CareerWay.JobAdvertService.Application.Features.Skills.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Skills.Queries.GetList;
using CareerWay.JobAdvertService.Domain.Entities;

namespace CareerWay.JobAdvertService.Application.Features.Skills.Mappings;

public class SkillMappingProfile : Profile
{
    public SkillMappingProfile()
    {
        CreateMap<Skill, GetSkillListItemResponse>().ReverseMap();
        CreateMap<Skill, CreateSkillCommand>().ReverseMap();
        CreateMap<Skill, CreateSkillResponse>().ReverseMap();
        CreateMap<Skill, EditSkillCommand>().ReverseMap();
        CreateMap<Skill, EditSkillResponse>().ReverseMap();
        CreateMap<Skill, SkillCreatedIntegrationEvent>().ReverseMap();
        CreateMap<Skill, SkillEditedIntegrationEvent>().ReverseMap();
    }
}
