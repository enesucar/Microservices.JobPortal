using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobAdvertService.Domain.Entities;

public class City : Entity
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
}
