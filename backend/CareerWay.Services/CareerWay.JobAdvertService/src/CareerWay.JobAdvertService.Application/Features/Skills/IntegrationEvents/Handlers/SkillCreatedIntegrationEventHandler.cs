using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Skills.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;

namespace CareerWay.JobAdvertService.Application.Features.Skills.IntegrationEvents.Handlers;

public class SkillCreatedIntegrationEventHandler : IIntegrationEventHandler<SkillCreatedIntegrationEvent>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;

    public SkillCreatedIntegrationEventHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task HandleAsync(SkillCreatedIntegrationEvent integrationEvent)
    {
        var skill = _mapper.Map<Skill>(integrationEvent);
        await _context.Skills.InsertOneAsync(skill);
    }
}
