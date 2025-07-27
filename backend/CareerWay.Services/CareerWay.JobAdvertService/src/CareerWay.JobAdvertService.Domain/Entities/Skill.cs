using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobAdvertService.Domain.Entities;

public class Skill : Entity
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}
