using CareerWay.JobAdvertService.Application.Features.PostSectors.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;
using MongoDB.Driver;

namespace CareerWay.JobAdvertService.Application.Features.PostSectors.IntegrationEvents.EventHandlers;

public class PostSectorCreatedIntegrationEventHandler : IIntegrationEventHandler<PostSectorCreatedIntegrationEvent>
{
    private readonly IJobAdvertReadDbContext _context;

    public PostSectorCreatedIntegrationEventHandler(
        IJobAdvertReadDbContext context)
    {
        _context = context;
    }

    public async Task HandleAsync(PostSectorCreatedIntegrationEvent integrationEvent)
    {
        var filter = Builders<PostSector>.Filter.Eq(u => u.PostId, integrationEvent.PostId);
        await _context.PostSectors.DeleteManyAsync(filter);

        var postSectors = integrationEvent.Items.Select(o => new PostSector()
        {
            Id = o.Id,
            PostId = integrationEvent.PostId,
            SectorId = o.SectorId
        });
        await _context.PostSectors.InsertManyAsync(postSectors);
    }
}
