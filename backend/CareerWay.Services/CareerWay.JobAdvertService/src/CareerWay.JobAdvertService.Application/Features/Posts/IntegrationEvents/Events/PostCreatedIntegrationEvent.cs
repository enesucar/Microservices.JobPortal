using CareerWay.JobAdvertService.Domain.Enums;
using CareerWay.Shared.EventBus.Events;

namespace CareerWay.JobAdvertService.Application.Features.Posts.IntegrationEvents.Events;

public class PostCreatedIntegrationEvent : IntegrationEvent
{
    public long Id { get; set; }

    public long CompanyId { get; set; }

    public string Slug { get; set; } = default!;

    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public WorkingType WorkingType { get; set; }

    public PositionLevelType PositionLevelType { get; set; }

    public DriverLicenseType? DriverLicenseType { get; set; }

    public GenderType GenderType { get; set; }

    public long DepartmantId { get; set; }

    public string DepartmantName { get; set; }

    public long PositionId { get; set; }

    public string PositionName { get; set; }

    public int CityId { get; set; }

    public string CityName { get; set; }

    public decimal? MinSalary { get; set; }

    public decimal? MaxSalary { get; set; }

    public byte? MinAge { get; set; }

    public byte? MaxAge { get; set; }

    public ExperienceType ExperienceType { get; set; }

    public byte? MinExperienceYear { get; set; }

    public byte? MaxExperienceYear { get; set; }

    public bool IsDisabledOnly { get; set; }

    public long CompanyPackageId { get; set; }

    public DateTime CreationDate { get; set; }

    public PostCreatedIntegrationEvent(Guid correlationId, DateTime eventPublishDate) 
        : base(correlationId, eventPublishDate)
    {
    }
}
