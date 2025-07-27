using CareerWay.AuthenticationServer.Web.Clients;
using CareerWay.AuthenticationServer.Web.Interfaces;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var backendBaseUrl = builder.Configuration.GetValue<string>("BackendOptions:BaseUrl")!;
var identityServiceUrl = builder.Configuration.GetValue<string>("BackendOptions:IdentityServiceUrl")!;
var registrationServiceUrl = builder.Configuration.GetValue<string>("BackendOptions:RegistartionServiceUrl")!;

builder.Services.AddHttpClient<IIdentityClient, IdentityClient>(options =>
{
    options.BaseAddress = new Uri($"{backendBaseUrl}{identityServiceUrl}");
});

builder.Services.AddHttpClient<IRegistrationClient, RegistrationClient>(options =>
{
    options.BaseAddress = new Uri($"{backendBaseUrl}{registrationServiceUrl}");
});

builder.Services.AddNewtonsoftJsonSerializer(o => { });

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
}).AddInMemoryClients([
    new Client
    {
        ClientId = "company",
        ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
        AllowedGrantTypes = GrantTypes.Code,
        RedirectUris = { "https://localhost:3014/signin-company" },
        PostLogoutRedirectUris = { "https://localhost:3014/signout-company" },
        FrontChannelLogoutUri = "https://localhost:3014/signout-callback-company",
        AllowedCorsOrigins = { "https://localhost:3014" },
        AllowOfflineAccess = true,
        AllowedScopes = { "openid", "profile", ClaimTypes.Role },
        RequireConsent = false,
    },
    new Client
    {
        ClientId = "jobseeker",
        ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
        AllowedGrantTypes = GrantTypes.Code,
        RedirectUris = { "https://localhost:3014/signin-jobseeker" },
        PostLogoutRedirectUris = { "https://localhost:3014/signout-jobseeker" },
        FrontChannelLogoutUri = "https://localhost:3014/signout-callback-jobseeker",
        AllowedCorsOrigins = { "https://localhost:3014" },
        AllowOfflineAccess = true,
        AllowedScopes = { "openid", "profile", ClaimTypes.Role },
        RequireConsent = false,
    }
])
.AddInMemoryApiScopes(new ApiScope[]
{
    new ApiScope("weatherapi.read"),
    new ApiScope("weatherapi.write"),

})
.AddInMemoryIdentityResources([
    new IdentityResources.OpenId(),
    new IdentityResources.Profile(),
    new IdentityResources.Email(),
    new IdentityResources.Phone(),
    new IdentityResource{
        Name = ClaimTypes.Role,
        DisplayName = "Role",
        Description = "Role",
        UserClaims = { ClaimTypes.Role }
    }
])
.AddInMemoryApiResources([
    new ApiResource("weatherapi")
    {
        Scopes = new List<string> {"weatherapi.read", "weatherapi.write"},
        ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
        UserClaims = new List<string> { ClaimTypes.Role }
    }
]).AddProfileService<CustomProfileService>();

builder.Services.AddAuthentication();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseIdentityServer();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .RequireAuthorization()
   .WithStaticAssets();

app.Run();
