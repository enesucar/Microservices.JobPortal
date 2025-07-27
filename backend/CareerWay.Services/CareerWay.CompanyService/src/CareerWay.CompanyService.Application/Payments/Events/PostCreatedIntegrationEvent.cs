using CareerWay.Shared.EventBus.Events;

namespace CareerWay.CompanyService.Application.Payments.Events;

public class PostCreatedIntegrationEvent : IntegrationEvent
{
    public PostCreatedIntegrationEvent(Guid correlationId, DateTime eventPublishDate) : base(correlationId, eventPublishDate)
    {
    }

    public long CompanyPackageId { get; set; }
}
