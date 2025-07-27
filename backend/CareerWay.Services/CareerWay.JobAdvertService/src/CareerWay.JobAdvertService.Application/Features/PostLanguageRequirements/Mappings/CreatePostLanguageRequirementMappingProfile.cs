using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.PostLanguageRequirements.Queries.GetList;
using CareerWay.JobAdvertService.Domain.Entities;

namespace CareerWay.JobAdvertService.Application.Features.PostLanguageRequirements.Mappings;

public class CreatePostLanguageRequirementMappingProfile : Profile
{
    public CreatePostLanguageRequirementMappingProfile()
    {
        CreateMap<PostLanguageRequirement, GetPostLanguageRequirementItemListResponse>().ReverseMap();
    }
}
