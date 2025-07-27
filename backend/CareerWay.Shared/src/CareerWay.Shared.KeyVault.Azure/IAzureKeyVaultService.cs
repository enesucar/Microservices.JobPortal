using Azure.Security.KeyVault.Secrets;

namespace CareerWay.Shared.KeyVault.Azure;

public interface IAzureKeyVaultService : IKeyVaultService
{
    SecretClient SecretClient { get; }
}
