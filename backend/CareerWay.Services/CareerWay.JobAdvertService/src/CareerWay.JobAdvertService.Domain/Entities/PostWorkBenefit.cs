using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobAdvertService.Domain.Entities;

public class PostWorkBenefit : Entity
{
    public long Id { get; set; }

    public long PostId { get; set; }

    public string Name { get; set; } = default!;
}
