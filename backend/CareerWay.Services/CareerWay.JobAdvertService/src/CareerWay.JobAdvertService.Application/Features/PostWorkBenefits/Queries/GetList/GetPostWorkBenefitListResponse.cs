namespace CareerWay.JobAdvertService.Application.Features.PostWorkBenefits.Queries.GetList;

public class GetPostWorkBenefitListResponse
{
    public long PostId { get; set; }

    public List<GetPostWorkBenefitItemListResponse> Items { get; set; } = [];
}

public class GetPostWorkBenefitItemListResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}