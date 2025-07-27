using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobSeekerService.Domain.Entities;

public class JobSeekerReference : Entity
{
    public virtual Guid Id { get; set; }

    public virtual long JobSeekerId { get; set; }

    public virtual string FullName { get; set; } = default!;

    public virtual long? PositionId { get; set; }

    public virtual string? PhoneNumber { get; set; }

    public virtual string? CompanyName { get; set; }

    public Position Position { get; set; } = default!;
}
