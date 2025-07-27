using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Departmants.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;

namespace CareerWay.JobAdvertService.Application.Features.Departmants.IntegrationEvents.Handlers;

public class DepartmantCreatedIntegrationEventHandler : IIntegrationEventHandler<DepartmantCreatedIntegrationEvent>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;

    public DepartmantCreatedIntegrationEventHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task HandleAsync(DepartmantCreatedIntegrationEvent integrationEvent)
    {
        var departmant = _mapper.Map<Departmant>(integrationEvent);
        await _context.Departmants.InsertOneAsync(departmant);
    }
}
