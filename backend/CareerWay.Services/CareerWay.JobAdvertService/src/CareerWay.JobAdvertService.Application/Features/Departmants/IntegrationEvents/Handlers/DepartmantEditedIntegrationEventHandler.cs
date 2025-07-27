using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Departmants.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CareerWay.JobAdvertService.Application.Features.Departmants.IntegrationEvents.Handlers;

public class DepartmantEditedIntegrationEventHandler : IIntegrationEventHandler<DepartmantEditedIntegrationEvent>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<DepartmantEditedIntegrationEventHandler> _logger;

    public DepartmantEditedIntegrationEventHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper,
        ILogger<DepartmantEditedIntegrationEventHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task HandleAsync(DepartmantEditedIntegrationEvent integrationEvent)
    {
        var departmant = await _context.Departmants.AsQueryable().FirstOrDefaultAsync(o => o.Id == integrationEvent.Id);
        if (departmant == null)
        {
            _logger.LogError("The departmant {DepartmantId} was not found. CorrelationId: {CorrelationId}", integrationEvent.Id, integrationEvent.CorrelationId);
            return;
        }

        _mapper.Map(integrationEvent, departmant);

        var filter = Builders<Departmant>.Filter.Eq(field => field.Id, departmant.Id);
        await _context.Departmants.ReplaceOneAsync(filter, departmant);
    }
}
