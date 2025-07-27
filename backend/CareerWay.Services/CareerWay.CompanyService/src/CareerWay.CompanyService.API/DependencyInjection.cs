using Azure.Identity;
using CareerWay.CompanyService.Infrastructure.Consts;
using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.Serilog;
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

        services.AddCorrelationIdProvider();

        services.EnableRequestBuffering();

        services.AddRequestTime();

        //services.AddPushSerilogProperties();

        services.AddCorrelationIdProvider();

        services.AddCustomExceptionHandler();

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

        services.AddCustomApiVersioning();

        services.AddOpenApi("v1");

        services.AddAuthentication();

        services.AddHttpContextAccessor();

        services.AddHttpContextCurrentPrincipalAccessor();

        services.AddDiscoveryClient(configuration);

        return services;
    }
}

