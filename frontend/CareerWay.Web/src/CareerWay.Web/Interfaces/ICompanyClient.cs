using CareerWay.Web.Models.Companies;

namespace CareerWay.Web.Interfaces;

public interface ICompanyClient
{
    Task<CompanyDetailResponse> GetCompanyDetail(long companyId);

    Task<List<CompanyPackageResponse>> GetPackages(long companyId, bool? isUsed = null);
}
