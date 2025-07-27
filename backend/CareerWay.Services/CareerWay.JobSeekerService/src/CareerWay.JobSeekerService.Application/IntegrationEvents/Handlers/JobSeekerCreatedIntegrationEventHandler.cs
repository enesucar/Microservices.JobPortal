using AutoMapper;
using CareerWay.JobSeekerService.Application.IntegrationEvents.Events;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;

namespace CareerWay.JobSeekerService.Application.IntegrationEvents.Handlers;

public class JobSeekerCreatedIntegrationEventHandler : IIntegrationEventHandler<JobSeekerCreatedIntegrationEvent>
{
    private readonly IJobSeekerDbContext _context;
    private readonly IMapper _mapper;

    public JobSeekerCreatedIntegrationEventHandler(
        IJobSeekerDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task HandleAsync(JobSeekerCreatedIntegrationEvent integrationEvent)
    {
        var jobSeeker = _mapper.Map<JobSeeker>(integrationEvent);
        await _context.JobSeekers.AddAsync(jobSeeker);
        await _context.SaveChangesAsync();
    }
}
