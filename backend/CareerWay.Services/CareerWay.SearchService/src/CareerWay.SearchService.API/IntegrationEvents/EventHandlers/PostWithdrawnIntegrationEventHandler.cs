using CareerWay.SearchService.API.Consts;
using CareerWay.SearchService.API.Data;
using CareerWay.SearchService.API.Entities;
using CareerWay.SearchService.API.IntegrationEvents.Events;
using CareerWay.Shared.EventBus.Abstractions;
using Nest;

namespace CareerWay.SearchService.API.IntegrationEvents.EventHandlers;

public class PostWithdrawnIntegrationEventHandler : IIntegrationEventHandler<PostWithdrawnIntegrationEvent>
{
    private readonly IElasticClient _elasticClient;

    public PostWithdrawnIntegrationEventHandler(
        IElasticClientFactory elasticClient)
    {
        _elasticClient = elasticClient.Create();
    }

    public async Task HandleAsync(PostWithdrawnIntegrationEvent integrationEvent)
    {
        await _elasticClient.DeleteAsync<Application>(integrationEvent.Id, d => d.Index(ApplicationConsts.IndexName));
    }
}