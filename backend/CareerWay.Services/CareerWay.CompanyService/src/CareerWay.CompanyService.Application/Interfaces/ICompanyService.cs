using CareerWay.CompanyService.Application.Models;

namespace CareerWay.CompanyService.Application.Interfaces;

public interface ICompanyService
{
    Task<CompanyDetailResponse> GetDetail(long id);

    Task Create(CreateCompanyRequest request);
}
