using CareerWay.JobAdvertService.Domain.Enums;
using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobSeekerService.Domain.Entities;

public class JobSeekerSchool : Entity
{
    public virtual Guid Id { get; set; }

    public virtual long JobSeekerId { get; set; }

    public virtual string Name { get; set; } = default!;

    public virtual EducationLevelType EducationLevelType { get; set; }

    public virtual short StartYear { get; set; }

    public virtual short EndYear { get; set; }
}
