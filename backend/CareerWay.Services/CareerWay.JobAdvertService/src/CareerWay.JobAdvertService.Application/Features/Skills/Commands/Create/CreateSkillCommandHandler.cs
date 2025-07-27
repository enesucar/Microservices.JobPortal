using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Skills.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.SnowflakeId;
using CareerWay.Shared.TimeProviders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobAdvertService.Application.Features.Skills.Commands.Create;

public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, CreateSkillResponse>
{
    private readonly IJobAdvertWriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISnowflakeIdGenerator _snowflakeIdGenerator;
    private readonly IEventBus _eventBus;
    private readonly IDateTime _dateTime;
    private readonly ICorrelationId _correlationId;

    public CreateSkillCommandHandler(
        IJobAdvertWriteDbContext context,
        IMapper mapper,
        ISnowflakeIdGenerator snowflakeIdGenerator,
        IEventBus eventBus,
        IDateTime dateTime,
        ICorrelationId correlationId)
    {
        _context = context;
        _mapper = mapper;
        _snowflakeIdGenerator = snowflakeIdGenerator;
        _eventBus = eventBus;
        _dateTime = dateTime;
        _correlationId = correlationId;
    }

    public async Task<CreateSkillResponse> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Skills.AnyAsync(o => o.Name == request.Name))
        {
            throw new ConflictException();
        }

        var skill = _mapper.Map<Skill>(request);
        skill.Id = _snowflakeIdGenerator.Generate();

        await _context.Skills.AddAsync(skill);
        await _context.SaveChangesAsync();

        var skillCreatedIntegrationEvent = new SkillCreatedIntegrationEvent(_correlationId.Get(), _dateTime.Now);
        _mapper.Map(skill, skillCreatedIntegrationEvent);
        await _eventBus.PublishAsync(skillCreatedIntegrationEvent);

        return _mapper.Map<CreateSkillResponse>(skill);
    }
}
