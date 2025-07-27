using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Positions.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;

namespace CareerWay.JobAdvertService.Application.Features.Positions.IntegrationEvents.Handlers;

public class PositionCreatedIntegrationEventHandler : IIntegrationEventHandler<PositionCreatedIntegrationEvent>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;

    public PositionCreatedIntegrationEventHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task HandleAsync(PositionCreatedIntegrationEvent integrationEvent)
    {
        var position = _mapper.Map<Position>(integrationEvent);
        await _context.Positions.InsertOneAsync(position);
    }
}
