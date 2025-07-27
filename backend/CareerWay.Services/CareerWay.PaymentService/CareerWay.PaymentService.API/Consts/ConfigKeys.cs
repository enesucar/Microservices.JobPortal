namespace CareerWay.PaymentService.API.Consts;

public static class ConfigKeys
{
    public static class AzureKeyVault
    {
        public const string Uri = "AzureKeyVaultOptions:Uri";
        public const string ClientId = "AzureKeyVaultOptions:ClientId";
        public const string ClientSecret = "AzureKeyVaultOptions:ClientSecret";
        public const string DirectoryId = "AzureKeyVaultOptions:DirectoryId";
    }

    public static class IyzicoOptions
    {
        public const string BaseUrl = "IyzicoOptions:BaseUrl";
        public const string ApiKey = "IyzicoSandboxApiKey";
        public const string SecretKey = "IyzicoSandboxSecretKey";
    }

    public static class Seq
    {
        public const string ConnectionString = "SeqConnectionString";
    }

    public static class Kafka
    {
        public const string ProducerBootstrapServers = "KafkaProducerBootstrapServers";
        public const string ProducerSaslUsername = "KafkaProducerSaslUsername";
        public const string ProducerSaslPassword = "KafkaProducerSaslPassword";

        public const string ConsumerBootstrapServers = "KafkaConsumerBootstrapServers";
        public const string ConsumerSaslUsername = "KafkaConsumerSaslUsername";
        public const string ConsumerSaslPassword = "KafkaConsumerSaslPassword";

        public const string AdminClientBootstrapServers = "KafkaAdminClientBootstrapServers";
        public const string AdminClientSaslUsername = "KafkaAdminClientSaslUsername";
        public const string AdminClientPassword = "KafkaAdminClientPassword";

        public const string GroupId = "KafkaOptions:GroupId";
        public const string ClientId = "KafkaOptions:ClientId";
        public const string ProjectName = "KafkaOptions:ProjectName";
        public const string ServiceName = "KafkaOptions:ServiceName";
    }
}
