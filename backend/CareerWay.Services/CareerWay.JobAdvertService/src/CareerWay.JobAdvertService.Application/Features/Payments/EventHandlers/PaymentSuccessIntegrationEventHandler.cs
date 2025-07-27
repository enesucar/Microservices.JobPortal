using CareerWay.JobAdvertService.Application.Features.Payments.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.SnowflakeId;

namespace CareerWay.JobAdvertService.Application.Features.Payments.EventHandlers;

public class PaymentSuccessIntegrationEventHandler : IIntegrationEventHandler<PaymentSuccessIntegrationEvent>
{
    private readonly IJobAdvertWriteDbContext _jobAdvertWriteDbContext;
    private readonly IJobAdvertReadDbContext _jobAdvertReadDbContext;
    private readonly ISnowflakeIdGenerator _snowflakeIdGenerator;

    public PaymentSuccessIntegrationEventHandler(
        IJobAdvertReadDbContext jobAdvertReadDbContext,
        ISnowflakeIdGenerator snowflakeIdGenerator,
        IJobAdvertWriteDbContext jobAdvertWriteDbContext)
    {
        _jobAdvertWriteDbContext = jobAdvertWriteDbContext;
        _snowflakeIdGenerator = snowflakeIdGenerator;
        _jobAdvertReadDbContext = jobAdvertReadDbContext;
    }

    public async Task HandleAsync(PaymentSuccessIntegrationEvent integrationEvent)
    {
        //var companyPackage = new CompanyPackage()
        //{
        //    Id = _snowflakeIdGenerator.Generate(),
        //    CompanyId = integrationEvent.CompanyId,
        //    PackageId = integrationEvent.PackageId,
        //};

        //await _jobAdvertWriteDbContext.CompanyPackages.AddAsync(companyPackage);
        //await _jobAdvertWriteDbContext.SaveChangesAsync();
        //await _jobAdvertReadDbContext.CompanyPackages.InsertOneAsync(companyPackage);
    }
}
