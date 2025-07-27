using CareerWay.CompanyService.Application.Models;

namespace CareerWay.CompanyService.Application.Interfaces;

public interface ICompanyPackageService
{
    Task<List<CompanyPackageResponse>> GetCompanyPackages(long companyId, bool? isUsed);
}
