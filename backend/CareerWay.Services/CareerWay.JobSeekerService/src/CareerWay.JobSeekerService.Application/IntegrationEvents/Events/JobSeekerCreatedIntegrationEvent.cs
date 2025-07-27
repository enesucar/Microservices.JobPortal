using CareerWay.Shared.EventBus.Events;

namespace CareerWay.JobSeekerService.Application.IntegrationEvents.Events;

public class JobSeekerCreatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public DateTime CreationDate { get; set; }

    public JobSeekerCreatedIntegrationEvent(
        Guid correlationId,
        DateTime eventPublishDate)
        : base(correlationId, eventPublishDate)
    {
    }
}
