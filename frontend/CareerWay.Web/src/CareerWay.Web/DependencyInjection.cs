using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.Guids;
using CareerWay.Web.Clients;
using CareerWay.Web.Consts;
using CareerWay.Web.Controllers;
using CareerWay.Web.Interfaces;
using CareerWay.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Logging;
using System.IdentityModel.Tokens.Jwt;

namespace CareerWay.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration,
        ConfigureHostBuilder hostBuilder)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configuration, nameof(configuration));
        Guard.Against.Null(hostBuilder, nameof(hostBuilder));

        services.AddControllersWithViews();

        services.AddSequentialGuidGenerator(o => o.SequentialGuidType = SequentialGuidType.SequentialAtEnd);

        services.AddMachineTimeProvider();

        services.AddSecurity(o => { });

        services.AddHttpContextAccessor();

        services.AddHttpContextCurrentPrincipalAccessor();

        var backendBaseUrl = configuration.GetValue<string>("BackendOptions:BaseUrl")!;
        var identityServiceUrl = configuration.GetValue<string>("BackendOptions:IdentityServiceUrl")!;
        var jobAdvertServiceUrl = configuration.GetValue<string>("BackendOptions:JobAdvertServiceUrl")!;
        var paymentServiceUrl = configuration.GetValue<string>("BackendOptions:PaymentServiceUrl")!;
        var companyServiceUrl = configuration.GetValue<string>("BackendOptions:CompanyServiceUrl")!;
        var searchServiceUrl = configuration.GetValue<string>("BackendOptions:SearchServiceUrl")!;
        var jobSeekerServiceUrl = configuration.GetValue<string>("BackendOptions:JobSeekerServiceUrl")!;
        var applicationServiceUrl = configuration.GetValue<string>("BackendOptions:ApplicationServiceUrl")!;
        var mediaServiceUrl = configuration.GetValue<string>("BackendOptions:MediaServiceUrl")!;

        services.AddHttpClient<IIdentityClient, IdentityClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{identityServiceUrl}");
        });

        services.AddHttpClient<IJobAdvertClient, JobAdvertClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{jobAdvertServiceUrl}");
        });

        services.AddHttpClient<IPaymentClient, PaymentClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{paymentServiceUrl}");
        });

        services.AddHttpClient<ICompanyClient, CompanyClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{companyServiceUrl}");
        });

        services.AddHttpClient<ICompanyClient, CompanyClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{companyServiceUrl}");
        });

        services.AddHttpClient<ISearchClient, SearchClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{searchServiceUrl}");
        });

        services.AddHttpClient<IJobSeekerClient, JobSeekerClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{jobSeekerServiceUrl}");
        });

        services.AddHttpClient<IApplicationClient, ApplicationClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{applicationServiceUrl}");
        });


        services.AddHttpClient<IMediaClient, MediaClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{mediaServiceUrl}");
        });

        services.AddNewtonsoftJsonSerializer(o => { });

        services.AddScoped<IUser, User>();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

        services.AddAuthorization();

        IdentityModelEventSource.ShowPII = true;

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = SecurityConsts.CompanyAuthenticationSchemeName;
        }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddOpenIdConnect(SecurityConsts.JobSeekerAuthenticationSchemeName, options =>
        {
            options.GetClaimsFromUserInfoEndpoint = true;
            options.ClaimActions.MapAll();
            options.Authority = configuration["InteractiveServiceSettings2:AuthorityUrl"];
            options.ClientId = configuration["InteractiveServiceSettings2:ClientId"];
            options.ClientSecret = configuration["InteractiveServiceSettings2:ClientSecret"];
            options.CallbackPath = "/signin-jobseeker";
            options.SignedOutRedirectUri = "/signout-jobseeker";
            options.SignedOutCallbackPath = "/signout-callback-jobseeker";
            options.ResponseType = "code";
            options.UsePkce = true;
            options.ResponseMode = "query";
            options.SaveTokens = true;
        })
        .AddOpenIdConnect(SecurityConsts.CompanyAuthenticationSchemeName, options =>
        {
            options.GetClaimsFromUserInfoEndpoint = true;
            options.ClaimActions.MapAll();
            options.Authority = configuration["InteractiveServiceSettings:AuthorityUrl"];
            options.ClientId = configuration["InteractiveServiceSettings:ClientId"];
            options.ClientSecret = configuration["InteractiveServiceSettings:ClientSecret"];
            options.CallbackPath = "/signin-company";
            options.SignedOutRedirectUri = "/signout-company";
            options.SignedOutCallbackPath = "/signout-callback-company";
            options.ResponseType = "code";
            options.UsePkce = true;
            options.ResponseMode = "query";
            options.SaveTokens = true;
        });

        return services;
    }
}
