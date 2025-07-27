using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Sectors.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;

namespace CareerWay.JobAdvertService.Application.Features.Sectors.IntegrationEvents.Handlers;

public class SectorCreatedIntegrationEventHandler : IIntegrationEventHandler<SectorCreatedIntegrationEvent>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;

    public SectorCreatedIntegrationEventHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task HandleAsync(SectorCreatedIntegrationEvent integrationEvent)
    {
        var sector = _mapper.Map<Sector>(integrationEvent);
        await _context.Sectors.InsertOneAsync(sector);
    }
}
