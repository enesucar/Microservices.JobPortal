using CareerWay.CompanyService.Application.Models;
using CareerWay.CompanyService.Domain.Entities;

namespace CareerWay.CompanyService.Application.Interfaces;

public interface ICompanyMapper
{
    Company Map(CreateCompanyRequest request, Company company);

    CompanyDetailResponse Map(Company company, CompanyDetailResponse response);

    Company Map(EditCompanyRequest request, Company company);

    EditCompanyResponse Map(Company company, EditCompanyResponse response);
}
