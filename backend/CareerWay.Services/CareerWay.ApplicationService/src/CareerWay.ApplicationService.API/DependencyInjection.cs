using Azure.Identity;
using CareerWay.ApplicationService.API.Consts;
using CareerWay.ApplicationService.API.Data.Contexts;
using CareerWay.ApplicationService.API.Interfaces;
using CareerWay.ApplicationService.API.Services;
using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.Serilog;
using Confluent.Kafka;
using JobSeekerGrpcService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Steeltoe.Discovery.Client;

namespace CareerWay.ApplicationService.API;

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

        configuration.AddAzureKeyVault(
          new Uri(configuration[ConfigKeys.AzureKeyVault.Uri]!),
          new ClientSecretCredential(
              configuration[ConfigKeys.AzureKeyVault.TenantId],
              configuration[ConfigKeys.AzureKeyVault.ClientId],
              configuration[ConfigKeys.AzureKeyVault.ClientSecret]
          )
      );

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration[ConfigKeys.PostgreSQL.ConnectionString]!);
        });

        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });

        services
            .AddControllers()
            .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

        services.EnableRequestBuffering();

        services.AddRequestTime();

        services.AddSequentialGuidGenerator(o => o.SequentialGuidType = Shared.Guids.SequentialGuidType.SequentialAsBinary);

        services.AddMachineTimeProvider();

        //services.AddPushSerilogProperties();

        services.AddCorrelationIdProvider();

        services.AddCustomCorrelationId();

        //services.AddCustomExceptionHandler();

        services.AddNewtonsoftJsonSerializer(o => { });

        services.AddCustomHttpLogging(o =>
        {
            o.ExcludedUrls =
            [
                "/JobSeekerService/GetJobSeekerList",
                "/scalar/v1",
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

        services.AddSecurity(options =>
        {
            options.Issuer = configuration[ConfigKeys.Security.Issuer]!;
            options.Audience = configuration[ConfigKeys.Security.Audience]!;
            options.Expiration = int.Parse(configuration[ConfigKeys.Security.Expiration]!);
            options.SecurityKey = configuration[ConfigKeys.Security.SecurityKey]!;
        });

        services.AddDiscoveryClient(configuration);

        services.AddScoped<IUser, User>();

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


        services.AddGrpcClient<JobSeekerService.JobSeekerServiceClient>(options =>
        {
            options.Address = new Uri("https://localhost:3003");
        });

        services.AddScoped<IJobSeekerGrpcClient, JobSeekerGrpcClient>();

        return services;
    }
}
