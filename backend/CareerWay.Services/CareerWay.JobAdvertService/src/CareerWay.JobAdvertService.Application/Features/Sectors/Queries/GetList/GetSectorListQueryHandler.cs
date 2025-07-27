using AutoMapper;
using CareerWay.JobAdvertService.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CareerWay.JobAdvertService.Application.Features.Sectors.Queries.GetList;

public class GetSectorListQueryHandler : IRequestHandler<GetSectorListQuery, GetSectorListResponse>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;

    public GetSectorListQueryHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetSectorListResponse> Handle(GetSectorListQuery request, CancellationToken cancellationToken)
    {
        var sectors = await _context.Sectors.AsQueryable().ToListAsync();
        return new GetSectorListResponse()
        {
            Items = _mapper.Map<IEnumerable<GetSectorListItemResponse>>(sectors)
        };
    }
}
