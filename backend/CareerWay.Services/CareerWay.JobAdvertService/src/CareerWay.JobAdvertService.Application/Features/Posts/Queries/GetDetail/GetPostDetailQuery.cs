using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.Posts.Queries.GetById;

public class GetPostDetailQuery : IRequest<GetPostDetailResponse>
{
    public long Id { get; set; }
}
