using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.PostSectors.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.PostSectors.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.PostWorkBenefits.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.SnowflakeId;
using CareerWay.Shared.TimeProviders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobAdvertService.Application.Features.PostWorkBenefits.Commands.Create;

public class CreateWorkBenefitCommandHandler : IRequestHandler<CreateWorkBenefitCommand, Unit>
{
    private readonly IJobAdvertWriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISnowflakeIdGenerator _snowflakeIdGenerator;
    private readonly IEventBus _eventBus;
    private readonly IDateTime _dateTime;
    private readonly ICorrelationId _correlationId;

    public CreateWorkBenefitCommandHandler(
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

    public async Task<Unit> Handle(CreateWorkBenefitCommand request, CancellationToken cancellationToken)
    {
        await _context.PostWorkBenefits.Where(o => o.PostId == request.PostId).ExecuteDeleteAsync();

        var workBenefits = request.Items.Select(o => new PostWorkBenefit()
        {
            Id = _snowflakeIdGenerator.Generate(),
            PostId = request.PostId,
            Name = o.Name
        });

        await _context.PostWorkBenefits.AddRangeAsync(workBenefits);
        await _context.SaveChangesAsync();

        var postWorkBenefitCreatedIntegrationEvent = new PostWorkBenefitCreatedIntegrationEvent(_correlationId.Get(), _dateTime.Now)
        {
            PostId = request.PostId,
            Items = workBenefits.Select(o => new PostWorkBenefitItemCreatedIntegrationEvent()
            {
                Id = o.Id,
                Name = o.Name
            }).ToList()
        };

        await _eventBus.PublishAsync(postWorkBenefitCreatedIntegrationEvent);

        return Unit.Value;
    }
}
