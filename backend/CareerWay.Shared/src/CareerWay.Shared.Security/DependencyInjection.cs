using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.Security.Tokens;
using CareerWay.Shared.Security.Users;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddSecurity(
        this IServiceCollection services,
        Action<AccessTokenOptions> configureAccessTokenOptions)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configureAccessTokenOptions, nameof(configureAccessTokenOptions));

        return services
            .Configure(configureAccessTokenOptions)
            .AddSingleton<ITokenService, JwtService>()
            .AddScoped<ICurrentUser, CurrentUser>();
    }
}
