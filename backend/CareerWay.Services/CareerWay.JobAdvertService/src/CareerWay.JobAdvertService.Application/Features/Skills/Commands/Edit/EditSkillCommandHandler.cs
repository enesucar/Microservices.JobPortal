using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Cities.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Skills.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.TimeProviders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobAdvertService.Application.Features.Skills.Commands.Edit;

public class EditSkillCommandHandler : IRequestHandler<EditSkillCommand, EditSkillResponse>
{
    private readonly IJobAdvertWriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICorrelationId _correlationId;
    private readonly IEventBus _eventBus;
    private readonly IDateTime _dateTime;

    public EditSkillCommandHandler(
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

    public async Task<EditSkillResponse> Handle(EditSkillCommand request, CancellationToken cancellationToken)
    {
        if (await SkillNameExistsAsync(request.Id, request.Name))
        {
            throw new ConflictException();
        }

        var skill = await _context.Skills.FirstOrDefaultAsync(o => o.Id == request.Id);
        if (skill == null)
        {
            throw new NotFoundException();
        }

        _mapper.Map(request, skill);
        await _context.SaveChangesAsync();

        var skillEditedIntegrationEvent = new SkillEditedIntegrationEvent(_correlationId.Get(), _dateTime.Now);
        _mapper.Map(skill, skillEditedIntegrationEvent);
        await _eventBus.PublishAsync(skillEditedIntegrationEvent);

        return _mapper.Map<EditSkillResponse>(skill);
    }

    public async Task<bool> SkillNameExistsAsync(long id, string name)
    {
        return await _context.Skills.AnyAsync(o => o.Name == name && o.Id != id);
    }
}
