using CareerWay.Shared.Domain.Entities;

namespace CareerWay.CompanyService.Domain.Entities;

public class City : Entity
{
    public virtual int Id { get; set; } = default!;

    public virtual string Name { get; set; } = default!;
}
