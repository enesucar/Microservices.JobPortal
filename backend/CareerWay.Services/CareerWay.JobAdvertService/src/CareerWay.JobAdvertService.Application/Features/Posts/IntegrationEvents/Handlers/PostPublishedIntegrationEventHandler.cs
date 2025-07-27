using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Departmants.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Departmants.IntegrationEvents.Handlers;
using CareerWay.JobAdvertService.Application.Features.Posts.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.Posts.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CareerWay.JobAdvertService.Application.Features.Posts.IntegrationEvents.Handlers;

public class PostPublishedIntegrationEventHandler : IIntegrationEventHandler<PostPublishedIntegrationEvent>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<DepartmantEditedIntegrationEventHandler> _logger;

    public PostPublishedIntegrationEventHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper,
        ILogger<DepartmantEditedIntegrationEventHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task HandleAsync(PostPublishedIntegrationEvent integrationEvent)
    {
        var post = await _context.Posts.AsQueryable().FirstOrDefaultAsync(o => o.Id == integrationEvent.Id);
        if (post == null)
        {
            _logger.LogError("The departmant {DepartmantId} was not found. CorrelationId: {CorrelationId}", integrationEvent.Id, integrationEvent.CorrelationId);
            return;
        }

        post.PublicationDate = integrationEvent.PublicationDate;
        post.ExpirationDate = integrationEvent.ExpirationDate;

        var filter = Builders<Post>.Filter.Eq(field => field.Id, post.Id);
        await _context.Posts.ReplaceOneAsync(filter, post);
    }
}
