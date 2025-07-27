using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Cities.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.Cities.Commands.Edit;
using CareerWay.JobAdvertService.Application.Features.Cities.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Cities.Queries.GetList;
using CareerWay.JobAdvertService.Domain.Entities;

namespace CareerWay.JobAdvertService.Application.Features.Cities.Mappings;

public class CityMappingProfile : Profile
{
    public CityMappingProfile()
    {
        CreateMap<City, GetCityListItemResponse>().ReverseMap();
        CreateMap<City, CreateCityCommand>().ReverseMap();
        CreateMap<City, CreateCityResponse>().ReverseMap();
        CreateMap<City, EditCityCommand>().ReverseMap();
        CreateMap<City, EditCityResponse>().ReverseMap();
        CreateMap<City, CityCreatedIntegrationEvent>().ReverseMap();
        CreateMap<City, CityEditedIntegrationEvent>().ReverseMap();
    }
}
