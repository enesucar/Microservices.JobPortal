using CareerWay.Shared.EventBus.Events;

namespace CareerWay.JobAdvertService.Application.Features.Cities.IntegrationEvents.Events;

public class CityEditedIntegrationEvent : IntegrationEvent
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public CityEditedIntegrationEvent(
        Guid correlationId, DateTime eventPublishDate)
        : base(correlationId, eventPublishDate)
    {
    }
}
