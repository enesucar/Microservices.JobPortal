using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Cities.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CareerWay.JobAdvertService.Application.Features.Cities.IntegrationEvents.Handlers;

public class CityEditedIntegrationEventHandler : IIntegrationEventHandler<CityEditedIntegrationEvent>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CityEditedIntegrationEventHandler> _logger;

    public CityEditedIntegrationEventHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper,
        ILogger<CityEditedIntegrationEventHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task HandleAsync(CityEditedIntegrationEvent integrationEvent)
    {
        var city = await _context.Cities.AsQueryable().FirstOrDefaultAsync(o => o.Id == integrationEvent.Id);
        if (city == null)
        {
            _logger.LogError("The city {CityId} was not found. CorrelationId: {CorrelationId}", integrationEvent.Id, integrationEvent.CorrelationId);
            return;
        }

        _mapper.Map(integrationEvent, city);

        var filter = Builders<City>.Filter.Eq(field => field.Id, city.Id);
        await _context.Cities.ReplaceOneAsync(filter, city);
    }
}
