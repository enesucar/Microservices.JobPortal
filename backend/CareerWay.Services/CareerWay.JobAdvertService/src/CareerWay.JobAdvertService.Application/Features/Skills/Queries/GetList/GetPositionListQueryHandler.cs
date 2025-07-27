using AutoMapper;
using CareerWay.JobAdvertService.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CareerWay.JobAdvertService.Application.Features.Skills.Queries.GetList;

public class GetSkillListQueryHandler : IRequestHandler<GetSkillListQuery, GetSkillListResponse>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;

    public GetSkillListQueryHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetSkillListResponse> Handle(GetSkillListQuery request, CancellationToken cancellationToken)
    {
        var skills = await _context.Skills.AsQueryable().ToListAsync();
        return new GetSkillListResponse()
        {
            Items = _mapper.Map<IEnumerable<GetSkillListItemResponse>>(skills)
        };
    }
}
