using CareerWay.Shared.EventBus.Events;

namespace CareerWay.JobAdvertService.Application.Features.Posts.IntegrationEvents.Events;

public class PostPublishedIntegrationEvent : IntegrationEvent
{
    public long Id { get; set; }

    public DateTime PublicationDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public PostPublishedIntegrationEvent(Guid correlationId, DateTime eventPublishDate) : base(correlationId, eventPublishDate)
    {
    }
}
