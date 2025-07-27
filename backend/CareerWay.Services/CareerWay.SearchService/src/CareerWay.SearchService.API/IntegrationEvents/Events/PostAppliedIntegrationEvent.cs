using CareerWay.Shared.EventBus.Events;

namespace CareerWay.SearchService.API.IntegrationEvents.Events;

public class PostAppliedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; set; }

    public long PostId { get; set; }

    public long UserId { get; set; }

    public PostAppliedIntegrationEvent(Guid correlationId, DateTime eventPublishDate) : base(correlationId, eventPublishDate)
    {
    }
}
