using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Posts.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.Posts.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Posts.Queries.GetById;
using CareerWay.JobAdvertService.Domain.Entities;

namespace CareerWay.JobAdvertService.Application.Features.Posts.Mappings;

public class PostMappingProfile : Profile
{
    public PostMappingProfile()
    {
        CreateMap<Post, CreatePostCommand>().ReverseMap();
        CreateMap<Post, CreatePostResponse>().ReverseMap();
        CreateMap<Post, GetPostDetailResponse>().ReverseMap();
        CreateMap<Post, PostCreatedIntegrationEvent>().ReverseMap();
    }
}