using CareerWay.SearchService.API.Consts;
using CareerWay.SearchService.API.Data;
using CareerWay.SearchService.API.Entities;
using CareerWay.SearchService.API.IntegrationEvents.Events;
using CareerWay.Shared.EventBus.Abstractions;
using Nest;

namespace CareerWay.SearchService.API.IntegrationEvents.EventHandlers;

public class PostCreatedIntegrationEventHandler : IIntegrationEventHandler<PostCreatedIntegrationEvent>
{
    private readonly ElasticClient _elasticClient;

    public PostCreatedIntegrationEventHandler(
      IElasticClientFactory elasticClientFactory)
    {
        _elasticClient = elasticClientFactory.Create();
    }

    public async Task HandleAsync(PostCreatedIntegrationEvent integrationEvent)
    {
        var post = new Post()
        {
            Id = integrationEvent.Id,
            Title = integrationEvent.Title,
            CompanyId = integrationEvent.CompanyId,
            City = new City()
            {
                Id = integrationEvent.CityId,
                Name = integrationEvent.CityName,
            },
            Departmant = new Departmant()
            {
                Id = integrationEvent.DepartmantId,
                Name = integrationEvent.DepartmantName
            },
            Description = integrationEvent.Description,
            CreationDate = integrationEvent.CreationDate,
            EducationLevelTypes = integrationEvent.EducationLevelTypes,
            ExpirationDate = integrationEvent.ExpirationDate,
            IsDisabledOnly = integrationEvent.IsDisabledOnly,
            Position = new Position()
            {
                Id = integrationEvent.PositionId,
                Name = integrationEvent.PositionName
            },
            PositionLevelType = integrationEvent.PositionLevelType,
            //Sectors = integrationEvent.Sectors,
            Slug = integrationEvent.Slug,
            Status = integrationEvent.Status,
            WorkingType = integrationEvent.WorkingType
        };

        await _elasticClient.IndexAsync(post, o => o.Index(PostConsts.IndexName));
    }
}