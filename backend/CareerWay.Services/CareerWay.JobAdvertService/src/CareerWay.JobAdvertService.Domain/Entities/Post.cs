using CareerWay.JobAdvertService.Domain.Enums;
using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobAdvertService.Domain.Entities;

public class Post : Entity
{
    public long Id { get; set; }

    public long CompanyId { get; set; }

    public int PackageId { get; set; }

    public string Title { get; set; } = default!;

    public string Slug { get; set; } = default!;

    public string Description { get; set; } = default!;

    public string? CoverPhoto { get; set; }

    public WorkingType WorkingType { get; set; }

    public PositionLevelType PositionLevelType { get; set; }

    public DriverLicenseType? DriverLicenseType { get; set; }

    public GenderType GenderType { get; set; }

    public ExperienceType ExperienceType { get; set; }

    public long DepartmantId { get; set; }

    public long PositionId { get; set; }

    public int? CityId { get; set; }

    public int? MinSalary { get; set; }

    public int? MaxSalary { get; set; }

    public byte? MinAge { get; set; }

    public byte? MaxAge { get; set; }

    public byte? MinExperienceYear { get; set; }

    public byte? MaxExperienceYear { get; set; }

    public bool IsDisabledOnly { get; set; }

    public long ViewCount { get; set; }

    public string? RejectionReason { get; set; }

    public PostStatus Status { get; set; }

    public DateTime? PublicationDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }

    public bool IsDeleted { get; set; }
}

