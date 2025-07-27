using CareerWay.SearchService.API.Consts;
using CareerWay.SearchService.API.Data;
using CareerWay.SearchService.API.Entities;
using CareerWay.SearchService.API.IntegrationEvents.Events;
using CareerWay.Shared.EventBus.Abstractions;
using Nest;

namespace CareerWay.SearchService.API.IntegrationEvents.EventHandlers;

public class PostPublishedIntegrationEventHandler : IIntegrationEventHandler<PostPublishedIntegrationEvent>
{
    private readonly IElasticClient _elasticClient;

    public PostPublishedIntegrationEventHandler(
        IElasticClientFactory elasticClient)
    {
        _elasticClient = elasticClient.Create();
    }

    public async Task HandleAsync(PostPublishedIntegrationEvent integrationEvent)
    {
        var post = (await _elasticClient.GetAsync<Post>(integrationEvent.Id, o => o.Index(PostConsts.IndexName))).Source;
        if (post == null)
        {
            return;
        }

        post.PublicationDate = integrationEvent.PublicationDate;
        post.ExpirationDate = integrationEvent.ExpirationDate;
        post.Status = Enums.PostStatus.Approved;
        
        await _elasticClient.UpdateAsync<Post>(
               integrationEvent.Id,
               selector: o => o.Index(PostConsts.IndexName).Doc(post)
        );
    }
}
