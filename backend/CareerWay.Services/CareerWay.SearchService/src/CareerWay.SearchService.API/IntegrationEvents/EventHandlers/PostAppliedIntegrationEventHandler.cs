using CareerWay.SearchService.API.Consts;
using CareerWay.SearchService.API.Data;
using CareerWay.SearchService.API.Entities;
using CareerWay.SearchService.API.IntegrationEvents.Events;
using CareerWay.Shared.EventBus.Abstractions;
using Nest;

namespace CareerWay.SearchService.API.IntegrationEvents.EventHandlers;

public class PostAppliedIntegrationEventHandler : IIntegrationEventHandler<PostAppliedIntegrationEvent>
{
    private readonly ElasticClient _elasticClient;

    public PostAppliedIntegrationEventHandler(
      IElasticClientFactory elasticClientFactory)
    {
        _elasticClient = elasticClientFactory.Create();
    }

    public async Task HandleAsync(PostAppliedIntegrationEvent integrationEvent)
    {
        var application = new Application()
        {
            Id = integrationEvent.Id,
            UserId = integrationEvent.UserId,
            PostId = integrationEvent.PostId
        };

        await _elasticClient.IndexAsync(application, o => o.Index(ApplicationConsts.IndexName));
    }
}

