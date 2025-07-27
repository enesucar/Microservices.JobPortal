using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Positions.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.Positions.Commands.Edit;
using CareerWay.JobAdvertService.Application.Features.Positions.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Positions.Queries.GetList;
using CareerWay.JobAdvertService.Domain.Entities;

namespace CareerWay.JobAdvertService.Application.Features.Positions.Mappings;

public class PositionMappingProfile : Profile
{
    public PositionMappingProfile()
    {
        CreateMap<Position, GetPositionListItemResponse>().ReverseMap();
        CreateMap<Position, CreatePositionCommand>().ReverseMap();
        CreateMap<Position, CreatePositionResponse>().ReverseMap();
        CreateMap<Position, EditPositionCommand>().ReverseMap();
        CreateMap<Position, EditPositionResponse>().ReverseMap();
        CreateMap<Position, PositionCreatedIntegrationEvent>().ReverseMap();
        CreateMap<Position, PositionEditedIntegrationEvent>().ReverseMap();
    }
}
