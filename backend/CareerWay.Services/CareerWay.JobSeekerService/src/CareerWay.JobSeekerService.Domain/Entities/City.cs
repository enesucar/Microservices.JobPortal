using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobSeekerService.Domain.Entities;

public class City : Entity
{
    public virtual int Id { get; set; }

    public virtual string Name { get; set; } = default!;
}
