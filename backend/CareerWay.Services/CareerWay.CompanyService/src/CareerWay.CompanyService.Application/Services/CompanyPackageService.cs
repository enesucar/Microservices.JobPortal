using CareerWay.CompanyService.Application.Interfaces;
using CareerWay.CompanyService.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.CompanyService.Application.Services;

public class CompanyPackageService : ICompanyPackageService
{
    private readonly ICompanyDbContext _companyDbContext;

    public CompanyPackageService(ICompanyDbContext companyDbContext)
    {
        _companyDbContext = companyDbContext;
    }

    public async Task<List<CompanyPackageResponse>> GetCompanyPackages(long companyId, bool? isUsed)
    {
        var query = _companyDbContext.CompanyPackages.Where(o => o.CompanyId == companyId);
        if (isUsed != null)
        {
            query = query.Where(o => o.IsUsed == isUsed);
        }

        var companyPackages = await query.ToListAsync();
        return companyPackages.Select(o => new CompanyPackageResponse()
        {
            Id = o.Id,
            CompanyId = o.CompanyId,
            IsUsed = o.IsUsed,
            CreationDate = o.CreationDate,
            PackageId = o.PackageId,
        }).ToList();
    }
}
