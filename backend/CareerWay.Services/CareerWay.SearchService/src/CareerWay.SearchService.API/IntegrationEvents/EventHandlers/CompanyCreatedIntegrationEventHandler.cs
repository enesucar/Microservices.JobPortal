using CareerWay.SearchService.API.Consts;
using CareerWay.SearchService.API.Data;
using CareerWay.SearchService.API.Entities;
using CareerWay.SearchService.API.IntegrationEvents.Events;
using CareerWay.Shared.EventBus.Abstractions;
using Nest;

namespace CareerWay.SearchService.API.IntegrationEvents.EventHandlers;

public class CompanyCreatedIntegrationEventHandler : IIntegrationEventHandler<CompanyCreatedIntegrationEvent>
{
    private readonly ElasticClient _elasticClient;

    public CompanyCreatedIntegrationEventHandler(
      IElasticClientFactory elasticClientFactory)
    {
        _elasticClient = elasticClientFactory.Create(); 
    }

    public async Task HandleAsync(CompanyCreatedIntegrationEvent integrationEvent)
    {
        var company = new Company()
        {
            Id = integrationEvent.Id,
            Title = integrationEvent.Title,
            ProfilePhoto = integrationEvent.ProfilePhoto
        };

        await _elasticClient.IndexAsync(company, o => o.Index(CompanyConsts.IndexName));
    }
}
