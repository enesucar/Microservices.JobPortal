using AutoMapper;
using CareerWay.JobAdvertService.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CareerWay.JobAdvertService.Application.Features.Departmants.Queries.GetList;

public class GetDepartmantListQueryHandler : IRequestHandler<GetDepartmantListQuery, GetDepartmantListResponse>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;

    public GetDepartmantListQueryHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetDepartmantListResponse> Handle(GetDepartmantListQuery request, CancellationToken cancellationToken)
    {
        var departmants = await _context.Departmants.AsQueryable().ToListAsync();
        return new GetDepartmantListResponse()
        {
            Items = _mapper.Map<IEnumerable<GetDepartmantListItemResponse>>(departmants)
        };
    }
}