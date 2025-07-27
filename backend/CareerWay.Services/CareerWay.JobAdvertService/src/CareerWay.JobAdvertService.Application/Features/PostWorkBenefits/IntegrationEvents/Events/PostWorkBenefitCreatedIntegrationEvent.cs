using CareerWay.Shared.EventBus.Events;

namespace CareerWay.JobAdvertService.Application.Features.PostWorkBenefits.IntegrationEvents.Events;

public class PostWorkBenefitCreatedIntegrationEvent : IntegrationEvent
{
    public long PostId { get; set; }

    public List<PostWorkBenefitItemCreatedIntegrationEvent> Items { get; set; } = [];

    public PostWorkBenefitCreatedIntegrationEvent(
        Guid correlationId,
        DateTime eventPublishDate) : base(correlationId, eventPublishDate)
    {
    }
}

public class PostWorkBenefitItemCreatedIntegrationEvent
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}
