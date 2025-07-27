using CareerWay.Shared.EventBus.Events;

namespace CareerWay.JobSeekerService.Application.IntegrationEvents.Events;

public class SkillCreatedIntegrationEvent : IntegrationEvent
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;

    public SkillCreatedIntegrationEvent(
        Guid correlationId, DateTime eventPublishDate)
        : base(correlationId, eventPublishDate)
    {
    }
}
