using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CareerWay.IdentityService.Infrastructure.Data.Contexts;
using CareerWay.Shared.TimeProviders;
using CareerWay.Shared.Guids;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.IdentityService.Domain.Entities;
using CareerWay.IdentityService.Domain.Constants;

namespace Microsoft.AspNetCore.Builder;

public static class WebApplicationExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<IdentityDbContextInitializer>();
        await initializer.InitializeAsync();
        await initializer.SeedAsync();
    }
}

public class IdentityDbContextInitializer
{
    private readonly ILogger<IdentityDbContextInitializer> _logger;
    private readonly IdentityDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IDateTime _dateTime;
    private readonly IGuidGenerator _guidGenerator;
   /// <summary>
   /// private readonly IEventBus _eventBus;
   /// </summary>
   /// <param name="logger"></param>
   /// <param name="context"></param>
   /// <param name="userManager"></param>
   /// <param name="roleManager"></param>
   /// <param name="dateTime"></param>
   /// <param name="guidGenerator"></param>
   /// <param name=""></param>

    public IdentityDbContextInitializer(
        ILogger<IdentityDbContextInitializer> logger,
        IdentityDbContext context,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IDateTime dateTime,
        IGuidGenerator guidGenerator
        ///IEventBus eventBus
        )
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _dateTime = dateTime;
        _guidGenerator = guidGenerator;
        //_eventBus = eventBus;
    }

    public async Task InitializeAsync()
    {
        try
        {
            //await _context.Database.MigrateAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        await CreateRoleAsync(new Role(RoleConsts.Administrator));
        await CreateRoleAsync(new Role(RoleConsts.Company));
        await CreateRoleAsync(new Role(RoleConsts.JobSeeker));
    }

    private async Task CreateRoleAsync(Role role)
    {
        if (_roleManager.Roles.All(r => r.Name != role.Name))
        {
            await _roleManager.CreateAsync(role);
        }
    }
}
