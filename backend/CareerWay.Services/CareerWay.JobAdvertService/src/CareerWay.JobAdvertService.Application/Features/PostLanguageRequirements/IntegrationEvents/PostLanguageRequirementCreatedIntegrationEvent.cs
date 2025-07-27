using CareerWay.JobAdvertService.Domain.Enums;
using CareerWay.Shared.EventBus.Events;

namespace CareerWay.JobAdvertService.Application.Features.PostLanguageRequirements.IntegrationEvents;

public class PostLanguageRequirementCreatedIntegrationEvent : IntegrationEvent
{
    public long PostId { get; set; }

    public List<PostLanguageRequirementItemCreatedIntegrationEvent> Items { get; set; } = [];

    public PostLanguageRequirementCreatedIntegrationEvent(
        Guid correlationId,
        DateTime eventPublishDate) : base(correlationId, eventPublishDate)
    {
    }
}

public class PostLanguageRequirementItemCreatedIntegrationEvent
{
    public long Id { get; set; }

    public LanguageType LanguageType { get; set; }

    public ReadingLevelType ReadingLevelType { get; set; }

    public WritingLevelType WritingLevelType { get; set; }

    public SpeakingLevelType SpeakingLevelType { get; set; }
}
