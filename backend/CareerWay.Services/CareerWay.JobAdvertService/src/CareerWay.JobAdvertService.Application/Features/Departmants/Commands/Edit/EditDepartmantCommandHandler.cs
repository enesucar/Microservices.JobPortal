using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Cities.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Departmants.Commands.Edit;
using CareerWay.JobAdvertService.Application.Features.Departmants.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.TimeProviders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobAdvertService.Application.Features.Departmants.Commands;

public class EditDepartmantCommandHandler : IRequestHandler<EditDepartmantCommand, EditDepartmantResponse>
{
    private readonly IJobAdvertWriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICorrelationId _correlationId;
    private readonly IEventBus _eventBus;
    private readonly IDateTime _dateTime;
    public EditDepartmantCommandHandler(
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

    public async Task<EditDepartmantResponse> Handle(EditDepartmantCommand request, CancellationToken cancellationToken)
    {
        if (await DepartmantNameExistsAsync(request.Id, request.Name))
        {
            throw new ConflictException();
        }

        var departmant = await _context.Departmants.FirstOrDefaultAsync(o => o.Id == request.Id);
        if (departmant == null)
        {
            throw new NotFoundException();
        }

        _mapper.Map(request, departmant);
        await _context.SaveChangesAsync();

        var departmantEditedIntegrationEvent = new DepartmantEditedIntegrationEvent(_correlationId.Get(), _dateTime.Now);
        _mapper.Map(departmant, departmantEditedIntegrationEvent);
        await _eventBus.PublishAsync(departmantEditedIntegrationEvent);

        return _mapper.Map<EditDepartmantResponse>(departmant);
    }

    public async Task<bool> DepartmantNameExistsAsync(long id, string name)
    {
        return await _context.Departmants.AnyAsync(o => o.Name == name && o.Id != id);
    }
}
