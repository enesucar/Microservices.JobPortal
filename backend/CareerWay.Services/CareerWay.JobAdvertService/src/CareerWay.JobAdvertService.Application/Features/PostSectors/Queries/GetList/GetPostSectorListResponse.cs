namespace CareerWay.JobAdvertService.Application.Features.PostSectors.Queries.GetList;

public class GetPostSectorListResponse
{
    public long PostId { get; set; }

    public List<GetPostSectorItemListResponse> Items { get; set; } = [];
}

public class GetPostSectorItemListResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}