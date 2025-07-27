using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Cities.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;

namespace CareerWay.JobAdvertService.Application.Features.Cities.IntegrationEvents.Handlers;

public class CityCreatedIntegrationEventHandler : IIntegrationEventHandler<CityCreatedIntegrationEvent>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;

    public CityCreatedIntegrationEventHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task HandleAsync(CityCreatedIntegrationEvent integrationEvent)
    {
        var city = _mapper.Map<City>(integrationEvent);
        await _context.Cities.InsertOneAsync(city);
    }
}
