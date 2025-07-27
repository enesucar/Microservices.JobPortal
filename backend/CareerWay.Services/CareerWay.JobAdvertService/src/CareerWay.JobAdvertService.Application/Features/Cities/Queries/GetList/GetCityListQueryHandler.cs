using AutoMapper;
using CareerWay.JobAdvertService.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CareerWay.JobAdvertService.Application.Features.Cities.Queries.GetList;

public class GetCityListQueryHandler : IRequestHandler<GetCityListQuery, GetCityListResponse>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;

    public GetCityListQueryHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetCityListResponse> Handle(GetCityListQuery request, CancellationToken cancellationToken)
    {
        var cities = await _context.Cities.AsQueryable().ToListAsync();
        return new GetCityListResponse()
        {
            Items = _mapper.Map<IEnumerable<GetCityListItemResponse>>(cities)
        };
    }
}
