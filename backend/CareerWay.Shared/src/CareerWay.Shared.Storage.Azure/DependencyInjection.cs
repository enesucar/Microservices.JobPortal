using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.Storage;
using CareerWay.Shared.Storage.Azure;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddAzureBlobStorage(
        this IServiceCollection services,
        Action<AzureBlobStorageOptions> configureAzureBlobStorageOptions)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configureAzureBlobStorageOptions, nameof(configureAzureBlobStorageOptions));

        return services
            .Configure(configureAzureBlobStorageOptions)
            .AddSingleton<IAzureStorage, AzureStorage>()
            .AddSingleton<IStorage, AzureStorage>();
    }
}
