using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Sectors.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Sectors.IntegrationEvents.Handlers;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CareerWay.JobAdvertService.Application.Features.Sectors.IntegrationEvents.Handlers;

public class SectorEditedIntegrationEventHandler : IIntegrationEventHandler<SectorEditedIntegrationEvent>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<SectorEditedIntegrationEventHandler> _logger;

    public SectorEditedIntegrationEventHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper,
        ILogger<SectorEditedIntegrationEventHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task HandleAsync(SectorEditedIntegrationEvent integrationEvent)
    {
        var sector = await _context.Sectors.AsQueryable().FirstOrDefaultAsync(o => o.Id == integrationEvent.Id);
        if (sector == null)
        {
            _logger.LogError("The sector {SectorId} was not found. CorrelationId: {CorrelationId}", integrationEvent.Id, integrationEvent.CorrelationId);
            return;
        }

        _mapper.Map(integrationEvent, sector);

        var filter = Builders<Sector>.Filter.Eq(field => field.Id, sector.Id);
        await _context.Sectors.ReplaceOneAsync(filter, sector);
    }
}
