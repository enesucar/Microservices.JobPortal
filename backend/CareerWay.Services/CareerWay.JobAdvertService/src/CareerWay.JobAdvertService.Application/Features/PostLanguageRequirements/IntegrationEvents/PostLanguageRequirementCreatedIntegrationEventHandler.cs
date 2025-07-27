using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;
using MongoDB.Driver;

namespace CareerWay.JobAdvertService.Application.Features.PostLanguageRequirements.IntegrationEvents;

public class PostLanguageRequirementCreatedIntegrationEventHandler : IIntegrationEventHandler<PostLanguageRequirementCreatedIntegrationEvent>
{
    private readonly IJobAdvertReadDbContext _context;

    public PostLanguageRequirementCreatedIntegrationEventHandler(
        IJobAdvertReadDbContext context)
    {
        _context = context;
    }

    public async Task HandleAsync(PostLanguageRequirementCreatedIntegrationEvent integrationEvent)
    {
        var filter = Builders<PostLanguageRequirement>.Filter.Eq(u => u.PostId, integrationEvent.PostId);
        await _context.PostLanguageRequirements.DeleteManyAsync(filter);

        var languageRequirements = integrationEvent.Items.Select(o => new PostLanguageRequirement()
        {
            Id = o.Id,
            PostId = integrationEvent.PostId,
            LanguageType = o.LanguageType,
            ReadingLevelType = o.ReadingLevelType,
            SpeakingLevelType = o.SpeakingLevelType,
            WritingLevelType = o.WritingLevelType
        });
        await _context.PostLanguageRequirements.InsertManyAsync(languageRequirements);
    }
}