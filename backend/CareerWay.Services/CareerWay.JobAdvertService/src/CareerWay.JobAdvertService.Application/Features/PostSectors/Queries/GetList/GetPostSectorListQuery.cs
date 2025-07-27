using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.PostSectors.Queries.GetList;

public class GetPostSectorListQuery : IRequest<GetPostSectorListResponse>
{
    public long PostId { get; set; }
}

