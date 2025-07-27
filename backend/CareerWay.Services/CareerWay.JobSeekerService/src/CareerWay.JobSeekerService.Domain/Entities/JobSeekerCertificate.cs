using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobSeekerService.Domain.Entities;

public class JobSeekerCertificate : Entity
{
    public virtual Guid Id { get; set; }

    public virtual long JobSeekerId { get; set; }

    public virtual string Name { get; set; } = default!;

    public virtual string? Description { get; set; }

    public virtual short ObtainedYear { get; set; }

    public virtual short ObtainedMonth { get; set; }
}
