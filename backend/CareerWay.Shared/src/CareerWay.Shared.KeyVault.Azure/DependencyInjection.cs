using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.KeyVault;
using CareerWay.Shared.KeyVault.Azure;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddAzureKeyVault(
        this IServiceCollection services,
        Action<AzureKeyVaultOptions> configureAzureKeyVaultOptions)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configureAzureKeyVaultOptions, nameof(configureAzureKeyVaultOptions));

        return services
            .Configure(configureAzureKeyVaultOptions)
            .AddSingleton<IKeyVaultService, AzureKeyVaultService>();
    }
}
