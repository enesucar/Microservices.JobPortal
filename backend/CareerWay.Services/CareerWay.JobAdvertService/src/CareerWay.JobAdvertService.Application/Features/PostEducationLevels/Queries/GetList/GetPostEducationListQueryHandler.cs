using AutoMapper;
using CareerWay.JobAdvertService.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CareerWay.JobAdvertService.Application.Features.PostEducationLevels.Queries.GetList;

public class GetPostEducationListQueryHandler : IRequestHandler<GetPostEducationListQuery, GetPostEducationListResponse>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;

    public GetPostEducationListQueryHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetPostEducationListResponse> Handle(GetPostEducationListQuery request, CancellationToken cancellationToken)
    {
        var educationLevels = await _context.PostEducationLevels.AsQueryable().Where(o => o.PostId == request.PostId).ToListAsync();
        return new GetPostEducationListResponse()
        {
            PostId = request.PostId,
            Items = _mapper.Map<List<GetPostEducationItemListResponse>>(educationLevels)
        };
    }
}
