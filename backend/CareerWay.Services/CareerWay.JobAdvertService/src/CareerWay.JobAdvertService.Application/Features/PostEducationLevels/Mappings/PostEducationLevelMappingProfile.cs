using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.PostEducationLevels.Queries.GetList;
using CareerWay.JobAdvertService.Domain.Entities;

namespace CareerWay.JobAdvertService.Application.Features.PostEducationLevels.Mappings;

public class PostEducationLevelMappingProfile : Profile
{
    public PostEducationLevelMappingProfile()
    {
        CreateMap<PostEducationLevel, GetPostEducationItemListResponse>().ReverseMap();
    }
}
