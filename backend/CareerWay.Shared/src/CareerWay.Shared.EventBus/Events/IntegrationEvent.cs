namespace CareerWay.Shared.EventBus.Events;

public abstract class IntegrationEvent
{
    public Guid CorrelationId { get; set; }

    public DateTime EventPublishDate { get; set; }

    public IntegrationEvent(Guid correlationId, DateTime eventPublishDate)
    {
        CorrelationId = correlationId;
        EventPublishDate = eventPublishDate;
    }
}
