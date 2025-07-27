using CareerWay.Shared.EventBus.Events;

namespace CareerWay.JobAdvertService.Application.Features.PostSectors.IntegrationEvents.Events;

public class PostSectorCreatedIntegrationEvent : IntegrationEvent
{
    public long PostId { get; set; }

    public List<PostSectorItemCreatedIntegrationEvent> Items { get; set; } = [];

    public PostSectorCreatedIntegrationEvent(
        Guid correlationId,
        DateTime eventPublishDate) : base(correlationId, eventPublishDate)
    {
    }
}

public class PostSectorItemCreatedIntegrationEvent
{
    public long Id { get; set; }

    public long SectorId { get; set; }
}
