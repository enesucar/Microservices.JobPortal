using AutoMapper;
using CareerWay.JobSeekerService.Application.IntegrationEvents.Events;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;

namespace CareerWay.JobSeekerService.Application.IntegrationEvents.Handlers;

public class PositionCreatedIntegrationEventHandler : IIntegrationEventHandler<PositionCreatedIntegrationEvent>
{
    private readonly IJobSeekerDbContext _context;
    private readonly IMapper _mapper;

    public PositionCreatedIntegrationEventHandler(
        IJobSeekerDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task HandleAsync(PositionCreatedIntegrationEvent integrationEvent)
    {
        var position = _mapper.Map<Position>(integrationEvent);
        await _context.Positions.AddAsync(position);
        await _context.SaveChangesAsync();
    }
}
