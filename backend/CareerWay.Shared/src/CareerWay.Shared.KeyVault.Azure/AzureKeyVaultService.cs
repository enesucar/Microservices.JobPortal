using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;

namespace CareerWay.Shared.KeyVault.Azure;

public class AzureKeyVaultService : IAzureKeyVaultService
{
    public SecretClient SecretClient { get; }

    public AzureKeyVaultService(IOptions<AzureKeyVaultOptions> options)
    {
        SecretClient = new SecretClient(options.Value.KeyVaultUri, new ClientSecretCredential("1", "1we", "1123"));
    }

    public async Task<string> GetSecretAsync(string secretName)
    {
        KeyVaultSecret keyVaultSecret = await SecretClient.GetSecretAsync(secretName);
        return keyVaultSecret.Name;
    }

    public async Task SetSecretAsync(string secretName, string secretValue)
    {
        await SecretClient.SetSecretAsync(secretName, secretValue);
    }

    public async Task DeleteSecretAsync(string secretName)
    {
        await SecretClient.StartDeleteSecretAsync(secretName);
    }
}
