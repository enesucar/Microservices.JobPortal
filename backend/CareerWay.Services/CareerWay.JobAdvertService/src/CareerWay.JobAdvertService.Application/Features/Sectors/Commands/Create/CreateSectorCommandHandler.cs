using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Sectors.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.SnowflakeId;
using CareerWay.Shared.TimeProviders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobAdvertService.Application.Features.Sectors.Commands.Create;

public class CreateSectorCommandHandler : IRequestHandler<CreateSectorCommand, CreateSectorResponse>
{
    private readonly IJobAdvertWriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISnowflakeIdGenerator _snowflakeIdGenerator;
    private readonly IEventBus _eventBus;
    private readonly IDateTime _dateTime;
    private readonly ICorrelationId _correlationId;

    public CreateSectorCommandHandler(
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

    public async Task<CreateSectorResponse> Handle(CreateSectorCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Sectors.AnyAsync(o => o.Name == request.Name))
        {
            throw new ConflictException();
        }

        var sector = _mapper.Map<Sector>(request);
        sector.Id = _snowflakeIdGenerator.Generate();

        await _context.Sectors.AddAsync(sector);
        await _context.SaveChangesAsync();

        var sectorCreatedIntegrationEvent = new SectorCreatedIntegrationEvent(_correlationId.Get(), _dateTime.Now);
        _mapper.Map(sector, sectorCreatedIntegrationEvent);
        await _eventBus.PublishAsync(sectorCreatedIntegrationEvent);

        return _mapper.Map<CreateSectorResponse>(sector);
    }
}
