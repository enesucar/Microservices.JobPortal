namespace CareerWay.CompanyService.Application.Models;

public class CompanyPackageResponse
{
    public long Id { get; set; }

    public int PackageId { get; set; }

    public long CompanyId { get; set; }

    public bool IsUsed { get; set; }

    public DateTime CreationDate { get; set; }
}