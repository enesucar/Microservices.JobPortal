using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobSeekerService.Domain.Entities;

public class JobSeekerWorkExperience : Entity
{
    public virtual Guid Id { get; set; }

    public virtual long JobSeekerId { get; set; }

    public virtual int CityId { get; set; }

    public virtual long PositionId { get; set; }

    public virtual string JobTitle { get; set; } = default!;

    public virtual string CompanyTitle { get; set; } = default!;

    public virtual string Description { get; set; } = default!;

    public virtual short StartMonth { get; set; }

    public virtual short StartYear { get; set; }

    public virtual short EndMonth { get; set; }

    public virtual short EndYear { get; set; }

    public virtual bool StillWorking { get; set; }

    public virtual City City { get; set; } = default!;

    public virtual Position Position { get; set; } = default!;
}
