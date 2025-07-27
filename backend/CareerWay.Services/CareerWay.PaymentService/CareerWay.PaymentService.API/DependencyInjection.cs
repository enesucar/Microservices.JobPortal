using CareerWay.PaymentService.API.Consts;
using CareerWay.PaymentService.API.Interfaces;
using CareerWay.PaymentService.API.Models;
using CareerWay.PaymentService.API.Repositories;
using CareerWay.PaymentService.API.Services;
using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.Serilog;
using Confluent.Kafka;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Steeltoe.Discovery.Client;

namespace CareerWay.PaymentService.API;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        ConfigurationManager configuration,
        ConfigureHostBuilder hostBuilder)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configuration, nameof(configuration));
        Guard.Against.Null(hostBuilder, nameof(hostBuilder));

        var keyVaultUri = configuration.GetValue<string>(ConfigKeys.AzureKeyVault.Uri);
        var clientId = configuration.GetValue<string>(ConfigKeys.AzureKeyVault.ClientId);
        var clientSecret = configuration.GetValue<string>(ConfigKeys.AzureKeyVault.ClientSecret);

        configuration.AddAzureKeyVault(keyVaultUri, clientId, clientSecret, new DefaultKeyVaultSecretManager());

        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });

        services
            .AddControllers()
            .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

        services.AddNewtonsoftJsonSerializer(o => { });

        services.EnableRequestBuffering();

        services.AddRequestTime();

        services.AddCustomCorrelationId();

        services.AddCorrelationIdProvider();

        services.AddPushSerilogProperties();

        services.AddMachineTimeProvider();

        services.AddSequentialGuidGenerator();

        //services.AddCustomExceptionHandler();

        services.AddCustomHttpLogging(o =>
        {
            o.ExcludedUrls =
            [   "/scalar/v1",
                "/scalar/scalar.aspnetcore.js",
                "/scalar/scalar.js",
                "/openapi/v1.json",
                "/favicon.ico"
            ];
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

        services.AddHttpContextAccessor();

        services.AddHttpContextCurrentPrincipalAccessor();

        services.AddCustomApiVersioning();

        services.AddOpenApi("v1");

        services.AddAuthorization();

        services.AddScoped<IUser, User>();

        services
           .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.Authority = "https://localhost:3013";
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateAudience = false,
                   ValidateIssuer = false,
                   ValidateIssuerSigningKey = false,
                   ValidateLifetime = false,
                   
                   ValidTypes = new[] { "at+jwt" }
               };
           });

        services.AddScoped<IPaymentService, IyzicoPaymentService>();
        services.AddSingleton<IPackageRepository, PackageRepository>();
        services.Configure<IyzicoOptions>(o =>
        {
            o.ApiKey = configuration.GetValue<string>(ConfigKeys.IyzicoOptions.ApiKey)!;
            o.SecretKey = configuration.GetValue<string>(ConfigKeys.IyzicoOptions.SecretKey)!;
            o.BaseUrl = configuration.GetValue<string>(ConfigKeys.IyzicoOptions.BaseUrl)!;
        });

        services.AddSecurity(options =>
        {
            options.Issuer = configuration["AccessTokenOptions:Issuer"]!;
            options.Audience = configuration["AccessTokenOptions:Audience"]!;
            options.Expiration = Convert.ToInt32(configuration["AccessTokenOptions:Expiration"]);
            options.SecurityKey = configuration["AccessTokenSecurityKey"]!;
        });

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

        services.AddAuthentication();

        services.AddDiscoveryClient(configuration);

        return services;
    }
}
