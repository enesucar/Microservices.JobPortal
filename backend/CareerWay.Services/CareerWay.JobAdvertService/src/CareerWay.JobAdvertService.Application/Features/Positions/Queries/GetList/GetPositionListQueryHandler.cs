using AutoMapper;
using CareerWay.JobAdvertService.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CareerWay.JobAdvertService.Application.Features.Positions.Queries.GetList;

public class GetPositionListQueryHandler : IRequestHandler<GetPositionListQuery, GetPositionListResponse>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;

    public GetPositionListQueryHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetPositionListResponse> Handle(GetPositionListQuery request, CancellationToken cancellationToken)
    {
        var positions = await _context.Positions.AsQueryable().ToListAsync();
        return new GetPositionListResponse()
        {
            Items = _mapper.Map<IEnumerable<GetPositionListItemResponse>>(positions)
        };
    }
}
