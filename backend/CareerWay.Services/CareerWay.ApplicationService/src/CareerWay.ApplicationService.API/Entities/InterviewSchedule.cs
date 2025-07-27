using CareerWay.ApplicationService.API.Enums;
using CareerWay.Shared.Domain.Entities;

namespace CareerWay.ApplicationService.API.Entities;

public class InterviewSchedule : Entity
{
    public Guid Id { get; set; }

    public Guid ApplicationId { get; set; }

    public DateTime ScheduledDate { get; set; }

    public InterviewType Type { get; set; }

    public string Location { get; set; } = default!;

    public string? Note { get; set; }
}
