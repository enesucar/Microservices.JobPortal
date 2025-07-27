using CareerWay.JobSeekerService.Domain.Enums;
using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobSeekerService.Domain.Entities;

public class JobSeekerLanguage : Entity
{
    public virtual Guid Id { get; set; }

    public virtual long JobSeekerId { get; set; }

    public virtual LanguageType LanguageType { get; set; }

    public virtual ReadingLevelType ReadingLevelType { get; set; }

    public virtual WritingLevelType WritingLevelType { get; set; }

    public virtual SpeakingLevelType SpeakingLevelType { get; set; }
}
