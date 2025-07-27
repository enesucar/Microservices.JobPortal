using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Posts.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;

namespace CareerWay.JobAdvertService.Application.Features.Posts.IntegrationEvents.Handlers;

public class PostCreatedIntegrationEventHandler : IIntegrationEventHandler<PostCreatedIntegrationEvent>
{
    private readonly IJobAdvertReadDbContext _jobAdvertReadDbContext;
    private readonly IMapper _mapper;

    public PostCreatedIntegrationEventHandler(IJobAdvertReadDbContext jobAdvertReadDbContext, IMapper mapper)
    {
        _jobAdvertReadDbContext = jobAdvertReadDbContext;
        _mapper = mapper;
    }

    public async Task HandleAsync(PostCreatedIntegrationEvent integrationEvent)
    {
        var post = _mapper.Map<Post>(integrationEvent);
        await _jobAdvertReadDbContext.Posts.InsertOneAsync(post);
    }
}
