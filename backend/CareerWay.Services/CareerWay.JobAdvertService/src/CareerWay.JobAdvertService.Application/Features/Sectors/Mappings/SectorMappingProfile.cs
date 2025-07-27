using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Sectors.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.Sectors.Commands.Edit;
using CareerWay.JobAdvertService.Application.Features.Sectors.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Sectors.Queries.GetList;
using CareerWay.JobAdvertService.Domain.Entities;

namespace CareerWay.JobAdvertService.Application.Features.Sectors.Mappings;

public class SectorMappingProfile : Profile
{
    public SectorMappingProfile()
    {
        CreateMap<Sector, GetSectorListItemResponse>().ReverseMap();
        CreateMap<Sector, CreateSectorCommand>().ReverseMap();
        CreateMap<Sector, CreateSectorResponse>().ReverseMap();
        CreateMap<Sector, EditSectorCommand>().ReverseMap();
        CreateMap<Sector, EditSectorResponse>().ReverseMap();
        CreateMap<Sector, SectorCreatedIntegrationEvent>().ReverseMap();
        CreateMap<Sector, SectorEditedIntegrationEvent>().ReverseMap();
    }
}
