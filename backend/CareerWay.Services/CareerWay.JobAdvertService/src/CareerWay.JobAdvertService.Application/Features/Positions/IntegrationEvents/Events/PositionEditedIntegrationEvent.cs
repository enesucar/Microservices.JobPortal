using CareerWay.Shared.EventBus.Events;

namespace CareerWay.JobAdvertService.Application.Features.Positions.IntegrationEvents.Events;

public class PositionEditedIntegrationEvent : IntegrationEvent
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;

    public PositionEditedIntegrationEvent(
        Guid correlationId, DateTime eventPublishDate)
        : base(correlationId, eventPublishDate)
    {
    }
}
