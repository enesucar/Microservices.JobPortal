using Azure.Identity;
using CareerWay.SearchService.API.Data;
using CareerWay.SearchService.API.IntegrationEvents.EventHandlers;
using CareerWay.SearchService.API.IntegrationEvents.Events;
using CareerWay.SearchService.Infrastructure.Consts;
using Confluent.Kafka;
using Steeltoe.Discovery.Client;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(
     this IServiceCollection services,
     ConfigurationManager configuration,
     ConfigureHostBuilder hostBuilder)
    {
        configuration.AddAzureKeyVault(
            new Uri(configuration[ConfigKeys.AzureKeyVault.Uri]!),
            new ClientSecretCredential(
                configuration[ConfigKeys.AzureKeyVault.TenantId],
                configuration[ConfigKeys.AzureKeyVault.ClientId],
                configuration[ConfigKeys.AzureKeyVault.ClientSecret]
            )
        );

        services.ConfigureCustomRouteOptions();

        services
           .AddControllers()
           .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

        services.AddScoped<IElasticClientFactory, ElasticClientFactory>();
        services.AddDiscoveryClient(configuration);

        services.AddTransient<PostCreatedIntegrationEventHandler>();
        services.AddTransient<CompanyCreatedIntegrationEventHandler>();
        services.AddTransient<PostPublishedIntegrationEventHandler>();
        services.AddTransient<PostAppliedIntegrationEventHandler>();
        services.AddTransient<PostWithdrawnIntegrationEventHandler>();

        services.AddRequestTime();

        services.EnableRequestBuffering();

        services.AddNewtonsoftJsonSerializer(o => { });

        services.AddSequentialGuidGenerator();

        services.AddDiscoveryClient(configuration);

        services.AddCustomCorrelationId();
        
        services.AddMachineTimeProvider();

        services.AddCustomApiVersioning();

        services.AddCorrelationIdProvider();

        services.AddKafka(eventBusOptions =>
        {
            eventBusOptions.ServiceName = configuration[ConfigKeys.Kafka.ServiceName]!;
            eventBusOptions.ProjectName = configuration[ConfigKeys.Kafka.ProjectName]!;
        }, kafkaOptions =>
        {
            kafkaOptions.ProducerConfig.BootstrapServers = configuration[ConfigKeys.Kafka.ProducerBootstrapServers];
            kafkaOptions.ProducerConfig.SaslUsername = configuration[ConfigKeys.Kafka.ProducerSaslUsername];
            kafkaOptions.ProducerConfig.SaslPassword = configuration[ConfigKeys.Kafka.ProducerSaslPassword];
            kafkaOptions.ProducerConfig.SaslMechanism = SaslMechanism.Plain;
            kafkaOptions.ProducerConfig.SecurityProtocol = SecurityProtocol.SaslSsl;
            kafkaOptions.ProducerConfig.ClientId = ConfigKeys.Kafka.ClientId;
            kafkaOptions.ProducerConfig.AllowAutoCreateTopics = true;
            kafkaOptions.ProducerConfig.EnableMetricsPush = false;

            kafkaOptions.ConsumerConfig.BootstrapServers = configuration[ConfigKeys.Kafka.ConsumerBootstrapServers];
            kafkaOptions.ConsumerConfig.SaslUsername = configuration[ConfigKeys.Kafka.ConsumerSaslUsername];
            kafkaOptions.ConsumerConfig.SaslPassword = configuration[ConfigKeys.Kafka.ConsumerSaslPassword];
            kafkaOptions.ConsumerConfig.SaslMechanism = SaslMechanism.Plain;
            kafkaOptions.ConsumerConfig.SecurityProtocol = SecurityProtocol.SaslSsl;
            kafkaOptions.ConsumerConfig.GroupId = configuration[ConfigKeys.Kafka.GroupId];
            kafkaOptions.ConsumerConfig.AutoOffsetReset = AutoOffsetReset.Earliest;
            kafkaOptions.ConsumerConfig.ClientId = configuration[ConfigKeys.Kafka.ClientId];
            kafkaOptions.ConsumerConfig.AllowAutoCreateTopics = true;
            kafkaOptions.ConsumerConfig.PartitionAssignmentStrategy = PartitionAssignmentStrategy.RoundRobin;
            kafkaOptions.ConsumerConfig.EnableMetricsPush = false;

            kafkaOptions.AdminClientConfig.BootstrapServers = configuration[ConfigKeys.Kafka.AdminClientBootstrapServers];
            kafkaOptions.AdminClientConfig.SaslUsername = configuration[ConfigKeys.Kafka.AdminClientSaslUsername];
            kafkaOptions.AdminClientConfig.SaslPassword = configuration[ConfigKeys.Kafka.AdminClientPassword];
            kafkaOptions.AdminClientConfig.SaslMechanism = SaslMechanism.Plain;
            kafkaOptions.AdminClientConfig.SecurityProtocol = SecurityProtocol.SaslSsl;
            kafkaOptions.AdminClientConfig.ClientId = configuration[ConfigKeys.Kafka.ClientId];
            kafkaOptions.AdminClientConfig.AllowAutoCreateTopics = true;
            kafkaOptions.AdminClientConfig.EnableMetricsPush = false;
        });

        return services;
    }
}
