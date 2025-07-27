using CareerWay.JobAdvertService.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CareerWay.JobAdvertService.Application.Features.PostWorkBenefits.Queries.GetList;

public class GetPostWorkBenefitListQueryHandler : IRequestHandler<GetPostWorkBenefitListQuery, GetPostWorkBenefitListResponse>
{
    private readonly IJobAdvertReadDbContext _context;

    public GetPostWorkBenefitListQueryHandler(
        IJobAdvertReadDbContext context)
    {
        _context = context;
    }

    public async Task<GetPostWorkBenefitListResponse> Handle(GetPostWorkBenefitListQuery request, CancellationToken cancellationToken)
    {
        var workBenefits = await _context.PostWorkBenefits.AsQueryable().Where(o => o.PostId == request.PostId).ToListAsync();
        return new GetPostWorkBenefitListResponse()
        {
            PostId = request.PostId,
            Items = workBenefits.Select(o => new GetPostWorkBenefitItemListResponse()
            {
                Id = o.Id,
                Name = o.Name,
            }).ToList()
        };
    }
}
