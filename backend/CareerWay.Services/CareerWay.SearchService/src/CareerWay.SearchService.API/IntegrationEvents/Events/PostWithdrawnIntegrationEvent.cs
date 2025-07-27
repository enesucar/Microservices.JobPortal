using CareerWay.Shared.EventBus.Events;

namespace CareerWay.SearchService.API.IntegrationEvents.Events;

public class PostWithdrawnIntegrationEvent : IntegrationEvent
{
    public PostWithdrawnIntegrationEvent(Guid correlationId, DateTime eventPublishDate) : base(correlationId, eventPublishDate)
    {
    }

    public Guid Id { get; set; }
}
