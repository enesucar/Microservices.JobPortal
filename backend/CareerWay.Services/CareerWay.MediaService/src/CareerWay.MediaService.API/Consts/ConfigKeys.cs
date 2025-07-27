namespace CareerWay.MediaService.API.Consts;

public static class ConfigKeys
{
    public static class AzureStorage
    {
        public const string AccountName = "AzureStorageAccountName";
        public const string AccountKey = "AzureStorageAccountKey";
        public const string ConnectionString = "AzureStorageConnectionString";
    }

    public static class AzureKeyVault
    {
        public const string Uri = "AzureKeyVaultOptions:Uri";
        public const string TenantId = "AzureKeyVaultOptions:TenantId";
        public const string ClientId = "AzureKeyVaultOptions:ClientId";
        public const string ClientSecret = "AzureKeyVaultOptions:ClientSecret";
    }

    public static class Seq
    {
        public const string ConnectionString = "SeqConnectionString";
    }
}
