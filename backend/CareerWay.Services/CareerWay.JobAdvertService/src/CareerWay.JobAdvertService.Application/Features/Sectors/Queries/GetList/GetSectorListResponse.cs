namespace CareerWay.JobAdvertService.Application.Features.Sectors.Queries.GetList;

public class GetSectorListResponse
{
    public IEnumerable<GetSectorListItemResponse> Items { get; set; } = [];
}

public class GetSectorListItemResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}
