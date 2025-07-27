using AutoMapper;
using CareerWay.JobSeekerService.Application.IntegrationEvents.Events;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.Shared.EventBus.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CareerWay.JobSeekerService.Application.IntegrationEvents.Handlers;

public class PositionEditedIntegrationEventHandler : IIntegrationEventHandler<PositionEditedIntegrationEvent>
{
    private readonly IJobSeekerDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<PositionEditedIntegrationEventHandler> _logger;

    public PositionEditedIntegrationEventHandler(
        IJobSeekerDbContext context,
        IMapper mapper,
        ILogger<PositionEditedIntegrationEventHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task HandleAsync(PositionEditedIntegrationEvent integrationEvent)
    {
        var position = await _context.Positions.FirstOrDefaultAsync(o => o.Id == integrationEvent.Id);
        if (position == null)
        {
            _logger.LogError("The position {PositionId} was not found. CorrelationId: {CorrelationId}", integrationEvent.Id, integrationEvent.CorrelationId);
            return;
        }
        _mapper.Map(integrationEvent, position);
        await _context.SaveChangesAsync();
    }
}