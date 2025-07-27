using CareerWay.Shared.EventBus.Events;

namespace CareerWay.JobAdvertService.Application.Features.Positions.IntegrationEvents.Events;

public class PositionCreatedIntegrationEvent : IntegrationEvent
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;

    public PositionCreatedIntegrationEvent(
        Guid correlationId, DateTime eventPublishDate)
        : base(correlationId, eventPublishDate)
    {
    }
}
