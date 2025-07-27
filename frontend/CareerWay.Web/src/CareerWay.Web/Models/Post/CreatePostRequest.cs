using CareerWay.Web.Enums;
using Web.Domain.Enums;

namespace CareerWay.Web.Models.Post;

public class CreatePostRequest
{
    public long Id { get; set; }

    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public int PackageId { get; set; }

    public long CompanyPackageId { get; set; }

    public WorkingType WorkingType { get; set; }

    public PositionLevelType PositionLevelType { get; set; }

    public DriverLicenseType DriverLicenseType { get; set; }

    public GenderType GenderType { get; set; }

    public long DepartmantId { get; set; }

    public long PositionId { get; set; }

    public int CityId { get; set; }

    public decimal? MinSalary { get; set; }

    public decimal? MaxSalary { get; set; }

    public byte? MinAge { get; set; }

    public byte? MaxAge { get; set; }

    public ExperienceType ExperienceType { get; set; }

    public byte? MinExperienceYear { get; set; }

    public byte? MaxExperienceYear { get; set; }

    public bool IsDisabledOnly { get; set; }
}
