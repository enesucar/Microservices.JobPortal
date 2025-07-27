using CareerWay.JobAdvertService.Domain.Enums;

namespace CareerWay.JobAdvertService.Application.Features.PostLanguageRequirements.Queries.GetList;

public class GetPostLanguageRequirementListResponse
{
    public long PostId { get; set; }

    public List<GetPostLanguageRequirementItemListResponse> Items { get; set; } = [];
}

public class GetPostLanguageRequirementItemListResponse
{
    public long Id { get; set; }

    public ReadingLevelType ReadingLevelType { get; set; }

    public WritingLevelType WritingLevelType { get; set; }

    public SpeakingLevelType SpeakingLevelType { get; set; }
}
