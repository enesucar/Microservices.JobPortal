using AutoMapper;
using CareerWay.JobSeekerService.Application.IntegrationEvents.Events;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.Shared.EventBus.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CareerWay.JobSeekerService.Application.IntegrationEvents.Handlers;

public class SkillEditedIntegrationEventHandler : IIntegrationEventHandler<SkillEditedIntegrationEvent>
{
    private readonly IJobSeekerDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<SkillEditedIntegrationEventHandler> _logger;

    public SkillEditedIntegrationEventHandler(
        IJobSeekerDbContext context,
        IMapper mapper,
        ILogger<SkillEditedIntegrationEventHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task HandleAsync(SkillEditedIntegrationEvent integrationEvent)
    {
        var skill = await _context.Skills.FirstOrDefaultAsync(o => o.Id == integrationEvent.Id);
        if (skill == null)
        {
            _logger.LogError("The skill {SkillId} was not found. CorrelationId: {CorrelationId}", integrationEvent.Id, integrationEvent.CorrelationId);
            return;
        }
        _mapper.Map(integrationEvent, skill);
        await _context.SaveChangesAsync();
    }
}
