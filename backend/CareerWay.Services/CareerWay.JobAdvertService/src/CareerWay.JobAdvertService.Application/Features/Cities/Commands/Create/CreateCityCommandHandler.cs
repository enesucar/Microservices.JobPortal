using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Cities.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.SnowflakeId;
using CareerWay.Shared.TimeProviders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobAdvertService.Application.Features.Cities.Commands.Create;

public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CreateCityResponse>
{
    private readonly IJobAdvertWriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISnowflakeIdGenerator _snowflakeIdGenerator;
    private readonly IEventBus _eventBus;
    private readonly IDateTime _dateTime;
    private readonly ICorrelationId _correlationId;

    public CreateCityCommandHandler(
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

    public async Task<CreateCityResponse> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Cities.AnyAsync(o => o.Name == request.Name))
        {
            throw new ConflictException();
        }

        var city = _mapper.Map<City>(request);

        await _context.Cities.AddAsync(city);
        await _context.SaveChangesAsync();

        var cityCreatedIntegrationEvent = new CityCreatedIntegrationEvent(_correlationId.Get(), _dateTime.Now);
        _mapper.Map(city, cityCreatedIntegrationEvent);
        await _eventBus.PublishAsync(cityCreatedIntegrationEvent);

        return _mapper.Map<CreateCityResponse>(city);
    }
}
