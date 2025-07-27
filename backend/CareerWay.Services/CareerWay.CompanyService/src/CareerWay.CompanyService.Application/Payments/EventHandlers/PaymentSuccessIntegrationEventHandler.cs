using CareerWay.CompanyService.Application.Interfaces;
using CareerWay.CompanyService.Domain.Entities;
using CareerWay.JobAdvertService.Application.Features.Payments.Events;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.SnowflakeId;

namespace CareerWay.CompanyService.Application.Payments.EventHandlers;

public class PaymentSuccessIntegrationEventHandler : IIntegrationEventHandler<PaymentSuccessIntegrationEvent>
{
    private readonly ICompanyDbContext _companyDbContext;
    private readonly ISnowflakeIdGenerator _snowflakeIdGenerator;

    public PaymentSuccessIntegrationEventHandler(
        ISnowflakeIdGenerator snowflakeIdGenerator,
        ICompanyDbContext companyDbContext)
    {
        _snowflakeIdGenerator = snowflakeIdGenerator;
        _companyDbContext = companyDbContext;
    }

    public async Task HandleAsync(PaymentSuccessIntegrationEvent integrationEvent)
    {
        var companyPackage = new CompanyPackage()
        {
            Id = _snowflakeIdGenerator.Generate(),
            CompanyId = integrationEvent.CompanyId,
            PackageId = integrationEvent.PackageId,
            CreationDate = integrationEvent.CreationDate
        };

        await _companyDbContext.CompanyPackages.AddAsync(companyPackage);
        await _companyDbContext.SaveChangesAsync();
    }
}
