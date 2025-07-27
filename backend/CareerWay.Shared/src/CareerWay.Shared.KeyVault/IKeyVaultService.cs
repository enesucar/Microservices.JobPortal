namespace CareerWay.Shared.KeyVault;

public interface IKeyVaultService
{
    Task<string> GetSecretAsync(string secretName);

    Task SetSecretAsync(string secretName, string secretValue);

    Task DeleteSecretAsync(string secretName);
}
