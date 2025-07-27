using CareerWay.JobAdvertService.Application.Features.PostEducationLevels.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;
using MongoDB.Driver;

namespace CareerWay.JobAdvertService.Application.Features.PostEducationLevels.IntegrationEvents.EventHandlers;

public class PostEducationLevelCreatedIntegrationEventHandler : IIntegrationEventHandler<PostEducationLevelCreatedIntegrationEvent>
{
    private readonly IJobAdvertReadDbContext _context;

    public PostEducationLevelCreatedIntegrationEventHandler(
        IJobAdvertReadDbContext context)
    {
        _context = context;
    }

    public async Task HandleAsync(PostEducationLevelCreatedIntegrationEvent integrationEvent)
    {
        var filter = Builders<PostEducationLevel>.Filter.Eq(u => u.PostId, integrationEvent.PostId);
        await _context.PostEducationLevels.DeleteManyAsync(filter);

        var educationLevels = integrationEvent.Items.Select(o => new PostEducationLevel()
        {
            Id = o.Id,
            PostId = integrationEvent.PostId,
            EducationLevelType = o.EducationLevelType,
        });
        await _context.PostEducationLevels.InsertManyAsync(educationLevels);
    }
}
