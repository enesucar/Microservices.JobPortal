using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Cities.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.TimeProviders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobAdvertService.Application.Features.Cities.Commands.Edit;

public class EditCityCommandHandler : IRequestHandler<EditCityCommand, EditCityResponse>
{
    private readonly IJobAdvertWriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICorrelationId _correlationId;
    private readonly IEventBus _eventBus;
    private readonly IDateTime _dateTime;

    public EditCityCommandHandler(
        IJobAdvertWriteDbContext context,
        IMapper mapper,
        ICorrelationId correlationId,
        IEventBus eventBus,
        IDateTime dateTime)
    {
        _context = context;
        _mapper = mapper;
        _correlationId = correlationId;
        _eventBus = eventBus;
        _dateTime = dateTime;
    }

    public async Task<EditCityResponse> Handle(EditCityCommand request, CancellationToken cancellationToken)
    {
        if (await CityNameExistsAsync(request.Id, request.Name))
        {
            throw new ConflictException();
        }

        var city = await _context.Cities.FirstOrDefaultAsync(o => o.Id == request.Id);
        if (city == null)
        {
            throw new NotFoundException();
        }

        _mapper.Map(request, city);
        await _context.SaveChangesAsync();

        var cityEditedIntegrationEvent = new CityEditedIntegrationEvent(_correlationId.Get(), _dateTime.Now);
        _mapper.Map(city, cityEditedIntegrationEvent);
        await _eventBus.PublishAsync(cityEditedIntegrationEvent);

        return _mapper.Map<EditCityResponse>(city);
    }

    public async Task<bool> CityNameExistsAsync(long id, string name)
    {
        return await _context.Cities.AnyAsync(o => o.Name == name && o.Id != id);
    }
}
