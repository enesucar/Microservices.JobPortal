using CareerWay.Shared.EventBus.Events;

namespace CareerWay.UserRegistrationSaga.API.IntegrationEvents.Events;

public class CompanyCreatedIntegrationEvent : IntegrationEvent
{
    public CompanyCreatedIntegrationEvent(Guid correlationId, DateTime eventPublishDate) : base(correlationId, eventPublishDate)
    {
    }

    public long Id { get; set; }

    public string Title { get; set; } = default!;

    public string? ProfilePhoto { get; set; }
}
