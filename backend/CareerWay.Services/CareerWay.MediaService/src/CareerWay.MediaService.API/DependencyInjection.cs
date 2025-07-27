using Azure.Identity;
using CareerWay.MediaService.API;
using CareerWay.MediaService.API.Consts; 
using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.Serilog; 
using Microsoft.Extensions.Configuration.AzureKeyVault;
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
        Guard.Against.Null(hostBuilder, nameof(hostBuilder));

        var keyVaultUri = configuration.GetValue<string>(ConfigKeys.AzureKeyVault.Uri);
        var clientId = configuration.GetValue<string>(ConfigKeys.AzureKeyVault.ClientId);
        var clientSecret = configuration.GetValue<string>(ConfigKeys.AzureKeyVault.ClientSecret);

        configuration.AddAzureKeyVault(
            new Uri(configuration[ConfigKeys.AzureKeyVault.Uri]!),
            new ClientSecretCredential(
                configuration[ConfigKeys.AzureKeyVault.TenantId],
                configuration[ConfigKeys.AzureKeyVault.ClientId],
                configuration[ConfigKeys.AzureKeyVault.ClientSecret]
            )
        );
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

        services.AddPushSerilogProperties();

        services.AddCorrelationIdProvider();

        //services.AddCustomExceptionHandler();

        //services.AddCustomHttpLogging(o =>
        //{
        //    o.ExcludedUrls =
        //    [   "/scalar/v1",
        //        "/scalar/scalar.aspnetcore.js",
        //        "/scalar/scalar.js",
        //        "/openapi/v1.json",
        //        "/favicon.ico"
        //    ];
        //});

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

        services.AddMachineTimeProvider();

        services.AddCustomCorrelationId();

        services.AddSequentialGuidGenerator(o => {  });

        services.AddSecurity(options =>
        {
            options.Issuer = configuration["AccessTokenOptions:Issuer"]!;
            options.Audience = configuration["AccessTokenOptions:Audience"]!;
            options.Expiration = Convert.ToInt32(configuration["AccessTokenOptions:Expiration"]);
            options.SecurityKey = configuration["AccessTokenSecurityKey"]!;
        });

        services.Configure<AzureBlobStorageOptions>(o =>
        {
            o.AccountName = configuration[ConfigKeys.AzureStorage.AccountName]!;
            o.AccountKey = configuration[ConfigKeys.AzureStorage.AccountKey]!;
            o.ConnectionString = configuration[ConfigKeys.AzureStorage.ConnectionString]!;
        });

        services.AddDiscoveryClient(configuration);

        return services;
    }
}