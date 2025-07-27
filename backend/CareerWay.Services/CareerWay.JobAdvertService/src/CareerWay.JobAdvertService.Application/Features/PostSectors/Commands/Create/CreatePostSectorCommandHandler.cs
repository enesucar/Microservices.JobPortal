using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Positions.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.Positions.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.PostEducationLevels.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.PostLanguageRequirements.IntegrationEvents;
using CareerWay.JobAdvertService.Application.Features.PostSectors.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.SnowflakeId;
using CareerWay.Shared.TimeProviders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobAdvertService.Application.Features.PostSectors.Commands.Create;

public class CreatePostSectorCommandHandler : IRequestHandler<CreatePostSectorCommand, Unit>
{
    private readonly IJobAdvertWriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISnowflakeIdGenerator _snowflakeIdGenerator;
    private readonly IEventBus _eventBus;
    private readonly IDateTime _dateTime;
    private readonly ICorrelationId _correlationId;

    public CreatePostSectorCommandHandler(
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

    public async Task<Unit> Handle(CreatePostSectorCommand request, CancellationToken cancellationToken)
    {
        await _context.PostSectors.Where(o => o.PostId == request.PostId).ExecuteDeleteAsync();

        var postSectors = request.Items.Select(o => new PostSector()
        {
            Id = _snowflakeIdGenerator.Generate(),
            PostId = request.PostId,
            SectorId = o.SectorId
        });

        await _context.PostSectors.AddRangeAsync(postSectors);
        await _context.SaveChangesAsync();

        var postSectorCreatedIntegrationEvent = new PostSectorCreatedIntegrationEvent(_correlationId.Get(), _dateTime.Now)
        {
            PostId = request.PostId,
            Items = postSectors.Select(o => new PostSectorItemCreatedIntegrationEvent()
            {
                Id = o.Id,
                SectorId = o.SectorId
            }).ToList()
        };

        await _eventBus.PublishAsync(postSectorCreatedIntegrationEvent);

        return Unit.Value;
    }
}