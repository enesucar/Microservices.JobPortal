using CareerWay.JobSeekerService.Domain.Enums;

namespace CareerWay.JobSeekerService.Application.Models;

public class CreateJobSeekerLanguageRequest
{
    public LanguageType LanguageType { get; set; }

    public ReadingLevelType ReadingLevelType { get; set; }

    public WritingLevelType WritingLevelType { get; set; }

    public SpeakingLevelType SpeakingLevelType { get; set; }
}
