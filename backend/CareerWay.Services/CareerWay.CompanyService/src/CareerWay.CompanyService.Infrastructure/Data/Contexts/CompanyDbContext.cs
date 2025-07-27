using CareerWay.CompanyService.Application.Interfaces;
using CareerWay.CompanyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CareerWay.CompanyService.Infrastructure.Data.Contexts;

public class CompanyDbContext : DbContext, ICompanyDbContext
{
    public DbSet<City> Cities => Set<City>();

    public DbSet<Company> Companies => Set<Company>();

    public DbSet<CompanyPackage> CompanyPackages => Set<CompanyPackage>();

    public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
 }
