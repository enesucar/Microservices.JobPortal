using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.PostWorkBenefits.Queries.GetList;

public class GetPostWorkBenefitListQuery : IRequest<GetPostWorkBenefitListResponse>
{
    public long PostId { get; set; }
}
