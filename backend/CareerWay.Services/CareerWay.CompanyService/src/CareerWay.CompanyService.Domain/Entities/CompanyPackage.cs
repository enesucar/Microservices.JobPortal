using CareerWay.Shared.Domain.Entities;

namespace CareerWay.CompanyService.Domain.Entities;

public class CompanyPackage : Entity
{
    public long Id { get; set; }

    public int PackageId { get; set; }

    public long CompanyId { get; set; }

    public bool IsUsed { get; set; }

    public DateTime CreationDate { get; set; }

    public Company Company { get; set; } = default!;
}
