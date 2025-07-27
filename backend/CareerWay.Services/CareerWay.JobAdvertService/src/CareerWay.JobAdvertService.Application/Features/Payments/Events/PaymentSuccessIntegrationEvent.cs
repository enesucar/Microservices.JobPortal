using CareerWay.Shared.EventBus.Events;

namespace CareerWay.JobAdvertService.Application.Features.Payments.Events;

public class PaymentSuccessIntegrationEvent : IntegrationEvent
{
    public int PackageId { get; set; }

    public Guid CompanyId { get; set; }

    public PaymentSuccessIntegrationEvent(
        Guid correlationId,
        DateTime eventPublishDate) : base(correlationId, eventPublishDate)
    {
    }
}
