namespace CareerWay.JobAdvertService.Application.Features.Positions.Queries.GetList;

public class GetPositionListResponse
{
    public IEnumerable<GetPositionListItemResponse> Items { get; set; } = [];
}

public class GetPositionListItemResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}
