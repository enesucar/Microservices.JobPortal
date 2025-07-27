using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Skills.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CareerWay.JobAdvertService.Application.Features.Skills.IntegrationEvents.Handlers;

public class SkillEditedIntegrationEventHandler : IIntegrationEventHandler<SkillEditedIntegrationEvent>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<SkillEditedIntegrationEventHandler> _logger;

    public SkillEditedIntegrationEventHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper,
        ILogger<SkillEditedIntegrationEventHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task HandleAsync(SkillEditedIntegrationEvent integrationEvent)
    {
        var skill = await _context.Skills.AsQueryable().FirstOrDefaultAsync(o => o.Id == integrationEvent.Id);
        if (skill == null)
        {
            _logger.LogError("The skill {SkillId} was not found. CorrelationId: {CorrelationId}", integrationEvent.Id, integrationEvent.CorrelationId);
            return;
        }

        _mapper.Map(integrationEvent, skill);

        var filter = Builders<Skill>.Filter.Eq(field => field.Id, skill.Id);
        await _context.Skills.ReplaceOneAsync(filter, skill);
    }
}