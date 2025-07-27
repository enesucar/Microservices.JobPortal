using AutoMapper;
using CareerWay.JobSeekerService.Application.IntegrationEvents.Events;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;

namespace CareerWay.JobSeekerService.Application.IntegrationEvents.Handlers;

public class SkillCreatedIntegrationEventHandler : IIntegrationEventHandler<SkillCreatedIntegrationEvent>
{
    private readonly IJobSeekerDbContext _context;
    private readonly IMapper _mapper;

    public SkillCreatedIntegrationEventHandler(
        IJobSeekerDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task HandleAsync(SkillCreatedIntegrationEvent integrationEvent)
    {
        var skill = _mapper.Map<Skill>(integrationEvent);
        await _context.Skills.AddAsync(skill);
        await _context.SaveChangesAsync();
    }
}
