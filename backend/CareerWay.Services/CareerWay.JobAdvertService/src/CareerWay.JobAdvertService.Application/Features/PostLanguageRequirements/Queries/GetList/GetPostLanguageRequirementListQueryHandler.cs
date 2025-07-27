using AutoMapper;
using CareerWay.JobAdvertService.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CareerWay.JobAdvertService.Application.Features.PostLanguageRequirements.Queries.GetList;

public class GetPostLanguageRequirementListQueryHandler : IRequestHandler<GetPostLanguageRequirementListQuery, GetPostLanguageRequirementListResponse>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;

    public GetPostLanguageRequirementListQueryHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetPostLanguageRequirementListResponse> Handle(GetPostLanguageRequirementListQuery request, CancellationToken cancellationToken)
    {
        var languageRequirements = await _context.PostLanguageRequirements.AsQueryable().Where(o => o.PostId == request.PostId).ToListAsync();
        return new GetPostLanguageRequirementListResponse()
        {
            PostId = request.PostId,
            Items = _mapper.Map<List<GetPostLanguageRequirementItemListResponse>>(languageRequirements)
        };
    }
}
