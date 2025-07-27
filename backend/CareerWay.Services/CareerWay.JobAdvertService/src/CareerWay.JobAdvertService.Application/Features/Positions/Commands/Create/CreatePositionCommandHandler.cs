using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Positions.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.SnowflakeId;
using CareerWay.Shared.TimeProviders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobAdvertService.Application.Features.Positions.Commands.Create;

public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, CreatePositionResponse>
{
    private readonly IJobAdvertWriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISnowflakeIdGenerator _snowflakeIdGenerator;
    private readonly IEventBus _eventBus;
    private readonly IDateTime _dateTime;
    private readonly ICorrelationId _correlationId;

    public CreatePositionCommandHandler(
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

    public async Task<CreatePositionResponse> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Positions.AnyAsync(o => o.Name == request.Name))
        {
            throw new ConflictException();
        }

        var position = _mapper.Map<Position>(request);
        position.Id = _snowflakeIdGenerator.Generate();

        await _context.Positions.AddAsync(position);
        await _context.SaveChangesAsync();

        var positionCreatedIntegrationEvent = new PositionCreatedIntegrationEvent(_correlationId.Get(), _dateTime.Now);
        _mapper.Map(position, positionCreatedIntegrationEvent);
        await _eventBus.PublishAsync(positionCreatedIntegrationEvent);

        return _mapper.Map<CreatePositionResponse>(position);
    }
}
