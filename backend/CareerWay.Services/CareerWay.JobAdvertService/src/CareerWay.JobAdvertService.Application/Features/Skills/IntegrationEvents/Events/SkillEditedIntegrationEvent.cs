using CareerWay.Shared.EventBus.Events;

namespace CareerWay.JobAdvertService.Application.Features.Skills.IntegrationEvents.Events;

public class SkillEditedIntegrationEvent : IntegrationEvent
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;

    public SkillEditedIntegrationEvent(
        Guid correlationId, DateTime eventPublishDate)
        : base(correlationId, eventPublishDate)
    {
    }
}
