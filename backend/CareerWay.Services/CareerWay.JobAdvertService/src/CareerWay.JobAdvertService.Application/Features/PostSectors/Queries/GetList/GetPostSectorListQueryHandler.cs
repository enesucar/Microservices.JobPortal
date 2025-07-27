using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.PostEducationLevels.Queries.GetList;
using CareerWay.JobAdvertService.Application.Interfaces;
using MediatR;
 using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CareerWay.JobAdvertService.Application.Features.PostSectors.Queries.GetList;

public class GetPostSectorListQueryHandler : IRequestHandler<GetPostSectorListQuery, GetPostSectorListResponse>
{
    private readonly IJobAdvertReadDbContext _context;

    public GetPostSectorListQueryHandler(
        IJobAdvertReadDbContext context)
    {
        _context = context;
    }

    public async Task<GetPostSectorListResponse> Handle(GetPostSectorListQuery request, CancellationToken cancellationToken)
    {
        var postSectors = await _context.PostSectors.AsQueryable().Where(o => o.PostId == request.PostId).ToListAsync();
        var postSectorIds = postSectors.Select(ps => ps.SectorId).ToList();
        var sectors = await _context.Sectors.AsQueryable().Where(o => postSectorIds.Contains(o.Id)).ToListAsync();
        return new GetPostSectorListResponse()
        {
            PostId = request.PostId,
            Items = postSectors.Select(o => new GetPostSectorItemListResponse()
            {
                Id = o.Id,
                Name = sectors.FirstOrDefault(o => o.Id == o.Id)!.Name
            }).ToList()
        };
    }
}
