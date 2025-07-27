using CareerWay.JobAdvertService.Domain.Enums;
using CareerWay.Shared.EventBus.Events;

namespace CareerWay.JobAdvertService.Application.Features.PostEducationLevels.IntegrationEvents.Events;

public class PostEducationLevelCreatedIntegrationEvent : IntegrationEvent
{  
    public long PostId { get; set; }

    public List<PostEducationLevelItemCreatedIntegrationEvent> Items { get; set; } = []; 

    public PostEducationLevelCreatedIntegrationEvent(
        Guid correlationId,
        DateTime eventPublishDate) : base(correlationId, eventPublishDate)
    {
    }
}

public class PostEducationLevelItemCreatedIntegrationEvent
{
    public long Id { get; set; }

    public PostEducationLevelType EducationLevelType { get; set; }
}
