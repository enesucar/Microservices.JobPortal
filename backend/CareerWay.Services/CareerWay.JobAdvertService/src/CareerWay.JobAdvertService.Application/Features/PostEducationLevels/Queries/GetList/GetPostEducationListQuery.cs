using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.PostEducationLevels.Queries.GetList;

public class GetPostEducationListQuery : IRequest<GetPostEducationListResponse>
{
    public long PostId { get; set; }
}
