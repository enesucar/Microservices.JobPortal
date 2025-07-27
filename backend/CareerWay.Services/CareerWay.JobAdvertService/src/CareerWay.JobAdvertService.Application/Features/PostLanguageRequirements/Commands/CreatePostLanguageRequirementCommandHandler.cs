using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.PostEducationLevels.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.PostEducationLevels.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.PostLanguageRequirements.IntegrationEvents;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.SnowflakeId;
using CareerWay.Shared.TimeProviders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobAdvertService.Application.Features.PostLanguageRequirements.Commands;

public class CreatePostLanguageRequirementCommandHandler : IRequestHandler<CreatePostLanguageRequirementCommand, Unit>
{
    private readonly IJobAdvertWriteDbContext _context;
    private readonly ISnowflakeIdGenerator _snowflakeIdGenerator;
    private readonly IEventBus _eventBus;
    private readonly IDateTime _dateTime;
    private readonly ICorrelationId _correlationId;

    public CreatePostLanguageRequirementCommandHandler(
        IJobAdvertWriteDbContext context, 
        ISnowflakeIdGenerator snowflakeIdGenerator,
        IEventBus eventBus,
        IDateTime dateTime,
        ICorrelationId correlationId)
    {
        _context = context; 
        _snowflakeIdGenerator = snowflakeIdGenerator;
        _eventBus = eventBus;
        _dateTime = dateTime;
        _correlationId = correlationId;
    }

    public async Task<Unit> Handle(CreatePostLanguageRequirementCommand request, CancellationToken cancellationToken)
    {
        await _context.PostLanguageRequirements.Where(o => o.PostId == request.PostId).ExecuteDeleteAsync();

        var languageRequirements = request.Items.Select(o => new PostLanguageRequirement()
        {
            Id = _snowflakeIdGenerator.Generate(),
            PostId = request.PostId,
            LanguageType = o.LanguageType,
            ReadingLevelType = o.ReadingLevelType,
            WritingLevelType = o.WritingLevelType,
            SpeakingLevelType = o.SpeakingLevelType
        });

        await _context.PostLanguageRequirements.AddRangeAsync(languageRequirements);
        await _context.SaveChangesAsync();

        var languageRequirementIntegrationEvent = new PostLanguageRequirementCreatedIntegrationEvent(_correlationId.Get(), _dateTime.Now)
        {
            PostId = request.PostId,
            Items = languageRequirements.Select(o => new PostLanguageRequirementItemCreatedIntegrationEvent()
            {
                Id = o.Id,
                LanguageType = o.LanguageType,
                ReadingLevelType = o.ReadingLevelType,
                WritingLevelType = o.WritingLevelType,
                SpeakingLevelType = o.SpeakingLevelType
            }).ToList()
        };

        await _eventBus.PublishAsync(languageRequirementIntegrationEvent);

        return Unit.Value;
    }
}
