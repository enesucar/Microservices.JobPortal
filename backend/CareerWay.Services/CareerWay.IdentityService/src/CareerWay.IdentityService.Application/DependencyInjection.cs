using CareerWay.IdentityService.Application.Identity;
using CareerWay.IdentityService.Application.Interfaces;
using CareerWay.IdentityService.Application.Services;
using CareerWay.IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IUserValidator<User>, CustomUserValidator>();

        return services;
    }
}
