using Azure.Identity;
using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.Serilog;
using CareerWay.UserRegistrationSaga.API.Clients;
using CareerWay.UserRegistrationSaga.API.Interfaces;
using CareerWay.UserRegistrationSaga.Consts;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Steeltoe.Discovery.Client;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(
       this IServiceCollection services,
       ConfigurationManager configuration,
       ConfigureHostBuilder hostBuilder)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configuration, nameof(configuration));

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

        services.AddCustomCorrelationId();

        services.AddCorrelationIdProvider();

        services.EnableRequestBuffering();

        services.AddRequestTime();

        services.AddPushSerilogProperties();

        services.AddCorrelationIdProvider();

        services.AddCustomExceptionHandler();
        
        services.AddCustomApiVersioning();

        services.AddOpenApi("v1");

        services.AddDiscoveryClient(configuration);

        services.AddCustomHttpLogging(o =>
        {
            o.ExcludedUrls =
            [   "/scalar/v1",
                "/scalar/scalar.aspnetcore.js",
                "/scalar/scalar.js",
                "/openapi/v1.json",
                "/favicon.ico"
            ];
            o.ExcludedRequestHeaders = ["Authorization"];
            o.ExcludedResponseHeaders = ["Authorization"];
        });

        hostBuilder.UseSerilog((context, services, configuration) =>
        {
            configuration
            .WriteTo.Seq(context.Configuration.GetValue<string>(ConfigKeys.Seq.ConnectionString)!)
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext} {CorrelationId}] {Message:lj}{NewLine}{Exception}")
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Confluent.Kafka", LogEventLevel.Warning)
            .AddCustomEnriches();
        });

        services.AddMachineTimeProvider();

        services.AddNewtonsoftJsonSerializer(o => { });

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

        var backendBaseUrl = configuration[ConfigKeys.CareerWayServices.BaseUrl];
        var identityServiceUrl = configuration[ConfigKeys.CareerWayServices.IdentityServiceUrl];
        var jobSeekerServiceUrl = configuration[ConfigKeys.CareerWayServices.JobSeekerServiceUrl];
        var companyServiceUrl = configuration[ConfigKeys.CareerWayServices.CompanyServiceUrl];

        services.AddScoped<IIdentityClient, IdentityClient>();

        services.AddHttpClient<IIdentityClient, IdentityClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{identityServiceUrl}");
        });

        services.AddHttpClient<ICompanyClient, CompanyClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{companyServiceUrl}");
        });

        services.AddHttpClient<IJobSeekerClient, JobSeekerClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{jobSeekerServiceUrl}");
        });

        return services;
    }
}
