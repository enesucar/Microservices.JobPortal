using CareerWay.CompanyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.CompanyService.Application.Interfaces;

public interface ICompanyDbContext
{
    DbSet<City> Cities { get; }

    DbSet<Company> Companies { get; }

    DbSet<CompanyPackage> CompanyPackages { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
