using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Departmants.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.Departmants.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.SnowflakeId;
using CareerWay.Shared.TimeProviders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobAdvertService.Application.Features.Departmants.Commands;

public class CreateDepartmantCommandHandler : IRequestHandler<CreateDepartmantCommand, CreateDepartmantResponse>
{
    private readonly IJobAdvertWriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISnowflakeIdGenerator _snowflakeIdGenerator;
    private readonly IEventBus _eventBus;
    private readonly IDateTime _dateTime;
    private readonly ICorrelationId _correlationId;

    public CreateDepartmantCommandHandler(
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

    public async Task<CreateDepartmantResponse> Handle(CreateDepartmantCommand request, CancellationToken cancellationToken)
    {
        if (await DepartmantNameExistsAsync(request.Name))
        {
            throw new ConflictException();
        }

        var departmant = _mapper.Map<Departmant>(request);
        departmant.Id = _snowflakeIdGenerator.Generate();

        await _context.Departmants.AddAsync(departmant);
        await _context.SaveChangesAsync();

        var departmantCreatedIntegrationEvent = new DepartmantCreatedIntegrationEvent(_correlationId.Get(), _dateTime.Now);
        _mapper.Map(departmant, departmantCreatedIntegrationEvent);
        await _eventBus.PublishAsync(departmantCreatedIntegrationEvent);

        return _mapper.Map<CreateDepartmantResponse>(departmant);
    }

    public async Task<bool> DepartmantNameExistsAsync(string name)
    {
        return await _context.Departmants.AnyAsync(o => o.Name == name);
    }
}
