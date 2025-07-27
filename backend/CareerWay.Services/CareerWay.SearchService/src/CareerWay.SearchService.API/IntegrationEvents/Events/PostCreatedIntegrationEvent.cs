using CareerWay.SearchService.API.Entities;
using CareerWay.SearchService.API.Enums;
using CareerWay.Shared.EventBus.Events;
using Nest;

namespace CareerWay.SearchService.API.IntegrationEvents.Events;

public class PostCreatedIntegrationEvent : IntegrationEvent
{
    public PostCreatedIntegrationEvent(Guid correlationId, DateTime eventPublishDate) : base(correlationId, eventPublishDate)
    {
    }

    public long Id { get; set; }

    public long CompanyId { get; set; }

    public string Title { get; set; } = default!;

    public string Slug { get; set; } = default!;

    public string Description { get; set; } = default!;

    public WorkingType WorkingType { get; set; }

    public PositionLevelType PositionLevelType { get; set; }

    public List<PostEducationLevelType> EducationLevelTypes { get; set; } = [];

    public long DepartmantId { get; set; } = default!;

    public string DepartmantName { get; set; }

    public long PositionId { get; set; } = default!;

    public string PositionName { get; set; }

    public List<Sector> Sectors { get; set; } = [];

    public int CityId { get; set; }

    public string CityName { get; set; }

    public bool IsDisabledOnly { get; set; }

    public PostStatus Status { get; set; }

    public DateTime? PublicationDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public DateTime CreationDate { get; set; }
}
