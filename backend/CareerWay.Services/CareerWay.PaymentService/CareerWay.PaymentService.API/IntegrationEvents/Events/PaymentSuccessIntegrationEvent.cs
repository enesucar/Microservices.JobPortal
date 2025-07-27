using CareerWay.Shared.EventBus.Events;

namespace CareerWay.PaymentService.API.IntegrationEvents.Events;

public class PaymentSuccessIntegrationEvent : IntegrationEvent
{  
    public int PackageId { get; set; }

    public long CompanyId { get; set; }

    public DateTime CreationDate { get; set; }

    public PaymentSuccessIntegrationEvent(
        Guid correlationId,
        DateTime eventPublishDate) : base(correlationId, eventPublishDate)
    {
    }
}
