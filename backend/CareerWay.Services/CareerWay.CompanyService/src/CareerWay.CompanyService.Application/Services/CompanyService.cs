using CareerWay.CompanyService.Application.Interfaces;
using CareerWay.CompanyService.Application.Models;
using CareerWay.CompanyService.Domain.Entities;
using CareerWay.Shared.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.CompanyService.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyDbContext _companyDbContext;
    private readonly ICompanyMapper _companyMapper;

    public CompanyService(
        ICompanyDbContext companyDbContext,
        ICompanyMapper companyMapper)
    {
        _companyDbContext = companyDbContext;
        _companyMapper = companyMapper;
    }

    public async Task Create(CreateCompanyRequest request)
    {
        var company = _companyMapper.Map(request, new Company());
        await _companyDbContext.Companies.AddAsync(company);
        await _companyDbContext.SaveChangesAsync();
    }

    public async Task<CompanyDetailResponse> GetDetail(long id)
    {
        var company = await _companyDbContext.Companies.Include(o => o.City).FirstOrDefaultAsync(o => o.Id == id);
        if (company == null)
        {
            throw new NotFoundException();
        }
        return _companyMapper.Map(company, new CompanyDetailResponse());
    }
}
