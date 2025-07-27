using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Cities.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Positions.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.TimeProviders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobAdvertService.Application.Features.Positions.Commands.Edit;

public class EditPositionCommandHandler : IRequestHandler<EditPositionCommand, EditPositionResponse>
{
    private readonly IJobAdvertWriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICorrelationId _correlationId;
    private readonly IEventBus _eventBus;
    private readonly IDateTime _dateTime;

    public EditPositionCommandHandler(
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

    public async Task<EditPositionResponse> Handle(EditPositionCommand request, CancellationToken cancellationToken)
    {
        if (await PositionNameExistsAsync(request.Id, request.Name))
        {
            throw new ConflictException();
        }

        var position = await _context.Positions.FirstOrDefaultAsync(o => o.Id == request.Id);
        if (position == null)
        {
            throw new NotFoundException();
        }

        _mapper.Map(request, position);
        await _context.SaveChangesAsync();

        var positionEditedIntegrationEvent = new PositionEditedIntegrationEvent(_correlationId.Get(), _dateTime.Now);
        _mapper.Map(position, positionEditedIntegrationEvent);
        await _eventBus.PublishAsync(positionEditedIntegrationEvent);

        return _mapper.Map<EditPositionResponse>(position);
    }

    public async Task<bool> PositionNameExistsAsync(long id, string name)
    {
        return await _context.Positions
            .AnyAsync(o => o.Name == name && o.Id != id);
    }
}
