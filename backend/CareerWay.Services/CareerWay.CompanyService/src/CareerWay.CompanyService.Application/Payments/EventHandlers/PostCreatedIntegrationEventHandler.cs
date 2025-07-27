using CareerWay.CompanyService.Application.Interfaces;
using CareerWay.CompanyService.Application.Payments.Events;
using CareerWay.Shared.EventBus.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.CompanyService.Application.Payments.EventHandlers;

public class PostCreatedIntegrationEventHandler : IIntegrationEventHandler<PostCreatedIntegrationEvent>
{
    private readonly ICompanyDbContext _companyDbContext;

    public PostCreatedIntegrationEventHandler(ICompanyDbContext companyDbContext)
    {
        _companyDbContext = companyDbContext;
    }

    public async Task HandleAsync(PostCreatedIntegrationEvent integrationEvent)
    {
        var package = await _companyDbContext.CompanyPackages.FirstOrDefaultAsync(o => o.Id == integrationEvent.CompanyPackageId);
        package!.IsUsed = true;
        await _companyDbContext.SaveChangesAsync();
    }
}
