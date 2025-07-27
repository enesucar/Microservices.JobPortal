using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobSeekerService.Domain.Entities;

public class Skill : Entity
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}
