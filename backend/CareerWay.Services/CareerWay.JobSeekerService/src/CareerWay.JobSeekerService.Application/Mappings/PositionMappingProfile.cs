using AutoMapper;
using CareerWay.JobSeekerService.Application.IntegrationEvents.Events;
using CareerWay.JobSeekerService.Domain.Entities;

namespace CareerWay.JobSeekerService.Application.Mappings;

public class PositionMappingProfile : Profile
{
    public PositionMappingProfile()
    {
        CreateMap<Position, PositionCreatedIntegrationEvent>().ReverseMap();
        CreateMap<Position, PositionEditedIntegrationEvent>().ReverseMap();
    }
}
