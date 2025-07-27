using CareerWay.JobAdvertService.Domain.Enums;
using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobAdvertService.Domain.Entities;

public class PostLanguageRequirement : Entity
{
    public long Id { get; set; }

    public long PostId { get; set; }

    public LanguageType LanguageType { get; set; }

    public ReadingLevelType ReadingLevelType { get; set; }

    public WritingLevelType WritingLevelType { get; set; }

    public SpeakingLevelType SpeakingLevelType { get; set; }
}
