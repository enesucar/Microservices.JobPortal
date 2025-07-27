using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Positions.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CareerWay.JobAdvertService.Application.Features.Positions.IntegrationEvents.Handlers;

public class PositionEditedIntegrationEventHandler : IIntegrationEventHandler<PositionEditedIntegrationEvent>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<PositionEditedIntegrationEventHandler> _logger;

    public PositionEditedIntegrationEventHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper,
        ILogger<PositionEditedIntegrationEventHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task HandleAsync(PositionEditedIntegrationEvent integrationEvent)
    {
        var position = await _context.Positions.AsQueryable().FirstOrDefaultAsync(o => o.Id == integrationEvent.Id);
        if (position == null)
        {
            _logger.LogError("The position {PositionId} was not found. CorrelationId: {CorrelationId}", integrationEvent.Id, integrationEvent.CorrelationId);
            return;
        }

        _mapper.Map(integrationEvent, position);

        var filter = Builders<Position>.Filter.Eq(field => field.Id, position.Id);
        await _context.Positions.ReplaceOneAsync(filter, position);
    }
}