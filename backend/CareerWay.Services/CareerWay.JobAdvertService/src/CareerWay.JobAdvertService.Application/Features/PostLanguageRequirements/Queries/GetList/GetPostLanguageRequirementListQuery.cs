using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.PostLanguageRequirements.Queries.GetList;

public class GetPostLanguageRequirementListQuery : IRequest<GetPostLanguageRequirementListResponse>
{
    public long PostId { get; set; }
}
