namespace CareerWay.JobAdvertService.Application.Features.Skills.Queries.GetList;

public class GetSkillListResponse
{
    public IEnumerable<GetSkillListItemResponse> Items { get; set; } = [];
}

public class GetSkillListItemResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}
