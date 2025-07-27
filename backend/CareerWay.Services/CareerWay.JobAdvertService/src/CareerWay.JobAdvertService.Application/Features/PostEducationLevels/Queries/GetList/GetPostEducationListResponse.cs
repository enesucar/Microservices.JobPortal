using CareerWay.JobAdvertService.Domain.Enums;

namespace CareerWay.JobAdvertService.Application.Features.PostEducationLevels.Queries.GetList;

public class GetPostEducationListResponse
{
    public long PostId { get; set; }

    public List<GetPostEducationItemListResponse> Items { get; set; } = [];
}

public class GetPostEducationItemListResponse
{
    public long Id { get; set; }

    public PostEducationLevelType EducationLevelType { get; set; }
}
