using CareerWay.JobAdvertService.Application.Features.PostWorkBenefits.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;
using MongoDB.Driver;

namespace CareerWay.JobAdvertService.Application.Features.PostWorkBenefits.IntegrationEvents.EventHandlers;

public class PostWorkBenefitCreatedIntegrationEventHandler : IIntegrationEventHandler<PostWorkBenefitCreatedIntegrationEvent>
{
    private readonly IJobAdvertReadDbContext _context;

    public PostWorkBenefitCreatedIntegrationEventHandler(
        IJobAdvertReadDbContext context)
    {
        _context = context;
    }

    public async Task HandleAsync(PostWorkBenefitCreatedIntegrationEvent integrationEvent)
    {
        var filter = Builders<PostWorkBenefit>.Filter.Eq(u => u.PostId, integrationEvent.PostId);
        await _context.PostWorkBenefits.DeleteManyAsync(filter);

        var workBenefits = integrationEvent.Items.Select(o => new PostWorkBenefit()
        {
            Id = o.Id,
            PostId = integrationEvent.PostId,
            Name = o.Name
        });
        await _context.PostWorkBenefits.InsertManyAsync(workBenefits);
    }
}
