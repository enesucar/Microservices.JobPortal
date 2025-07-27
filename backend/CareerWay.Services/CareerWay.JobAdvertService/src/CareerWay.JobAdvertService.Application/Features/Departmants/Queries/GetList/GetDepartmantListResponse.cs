namespace CareerWay.JobAdvertService.Application.Features.Departmants.Queries.GetList;

public class GetDepartmantListResponse
{
    public IEnumerable<GetDepartmantListItemResponse> Items { get; set; } = [];
}

public class GetDepartmantListItemResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}
