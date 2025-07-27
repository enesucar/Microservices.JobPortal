using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Cities.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Sectors.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.TimeProviders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobAdvertService.Application.Features.Sectors.Commands.Edit;

public class EditSectorCommandHandler : IRequestHandler<EditSectorCommand, EditSectorResponse>
{
    private readonly IJobAdvertWriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICorrelationId _correlationId;
    private readonly IEventBus _eventBus;
    private readonly IDateTime _dateTime;

    public EditSectorCommandHandler(
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

    public async Task<EditSectorResponse> Handle(EditSectorCommand request, CancellationToken cancellationToken)
    {
        if (await SectorNameExistsAsync(request.Id, request.Name))
        {
            throw new ConflictException();
        }

        var sector = await _context.Sectors.FirstOrDefaultAsync(o => o.Id == request.Id);
        if (sector == null)
        {
            throw new NotFoundException();
        }

        _mapper.Map(request, sector);
        await _context.SaveChangesAsync();

        var sectorEditedIntegrationEvent = new SectorEditedIntegrationEvent(_correlationId.Get(), _dateTime.Now);
        _mapper.Map(sector, sectorEditedIntegrationEvent);
        await _eventBus.PublishAsync(sectorEditedIntegrationEvent);

        return _mapper.Map<EditSectorResponse>(sector);
    }

    public async Task<bool> SectorNameExistsAsync(long id, string name)
    {
        return await _context.Sectors.AnyAsync(o => o.Name == name && o.Id != id);
    }
}
