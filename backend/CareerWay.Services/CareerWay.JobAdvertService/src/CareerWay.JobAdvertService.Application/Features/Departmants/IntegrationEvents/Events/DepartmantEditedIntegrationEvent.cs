using CareerWay.Shared.EventBus.Events;

namespace CareerWay.JobAdvertService.Application.Features.Departmants.IntegrationEvents.Events;

public class DepartmantEditedIntegrationEvent : IntegrationEvent
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;

    public DepartmantEditedIntegrationEvent(
        Guid correlationId, DateTime eventPublishDate)
        : base(correlationId, eventPublishDate)
    {
    }
}
