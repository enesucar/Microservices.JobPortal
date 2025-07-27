using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobSeekerService.Domain.Entities;

public class JobSeekerProject : Entity
{
    public virtual Guid Id { get; set; }

    public virtual long JobSeekerId { get; set; }

    public virtual short ProjectYear { get; set; }

    public virtual short ProjectMonth { get; set; }

    public virtual string? Link { get; set; }

    public virtual string? Description { get; set; }
}
