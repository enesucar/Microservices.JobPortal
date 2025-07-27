using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.PostEducationLevels.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.SnowflakeId;
using CareerWay.Shared.TimeProviders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobAdvertService.Application.Features.PostEducationLevels.Commands.Create;

public class CreatePostEducationLevelCommandHandler : IRequestHandler<CreatePostEducationLevelCommand, Unit>
{
    private readonly IJobAdvertWriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISnowflakeIdGenerator _snowflakeIdGenerator;
    private readonly IEventBus _eventBus;
    private readonly IDateTime _dateTime;
    private readonly ICorrelationId _correlationId; 

    public CreatePostEducationLevelCommandHandler(
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

    public async Task<Unit> Handle(CreatePostEducationLevelCommand request, CancellationToken cancellationToken)
    {
        await _context.PostEducationLevels.Where(o => o.PostId == request.PostId).ExecuteDeleteAsync();

        var educationLevels = request.Items.Select(o => new PostEducationLevel()
        {
            Id = _snowflakeIdGenerator.Generate(),
            PostId = request.PostId,
            EducationLevelType = o.EducationLevelType
        });

        await _context.PostEducationLevels.AddRangeAsync(educationLevels);
        await _context.SaveChangesAsync();

        var postEducationLevelIntegrationEvent = new PostEducationLevelCreatedIntegrationEvent(_correlationId.Get(), _dateTime.Now)
        {
            PostId = request.PostId,
            Items = educationLevels.Select(o => new PostEducationLevelItemCreatedIntegrationEvent()
            {
                Id = o.Id,
                EducationLevelType = o.EducationLevelType
            }).ToList()
        };

        await _eventBus.PublishAsync(postEducationLevelIntegrationEvent);

        return Unit.Value;
    }
}
