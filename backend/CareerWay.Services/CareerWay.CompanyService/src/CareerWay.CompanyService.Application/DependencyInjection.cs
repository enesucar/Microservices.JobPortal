using CareerWay.CompanyService.Application.Interfaces;
using CareerWay.CompanyService.Application.Mappers;
using CareerWay.CompanyService.Application.Payments.EventHandlers;
using CareerWay.CompanyService.Application.Services;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICompanyPackageService, CompanyPackageService>();
        services.AddSingleton<ICompanyMapper, CompanyMapper>();
        services.AddTransient<PaymentSuccessIntegrationEventHandler>();
        services.AddTransient<PostCreatedIntegrationEventHandler>();
        
        return services;
    }
}
