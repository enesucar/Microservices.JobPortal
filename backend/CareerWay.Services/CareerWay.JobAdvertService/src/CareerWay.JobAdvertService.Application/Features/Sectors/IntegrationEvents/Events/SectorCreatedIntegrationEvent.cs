using CareerWay.Shared.EventBus.Events;

namespace CareerWay.JobAdvertService.Application.Features.Sectors.IntegrationEvents.Events;

public class SectorCreatedIntegrationEvent : IntegrationEvent
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;

    public SectorCreatedIntegrationEvent(
        Guid correlationId, DateTime eventPublishDate)
        : base(correlationId, eventPublishDate)
    {
    }
}
