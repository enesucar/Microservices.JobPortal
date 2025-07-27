namespace CareerWay.JobAdvertService.Application.Features.Cities.Queries.GetList;

public class GetCityListResponse
{
    public IEnumerable<GetCityListItemResponse> Items { get; set; } = [];
}

public class GetCityListItemResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}
