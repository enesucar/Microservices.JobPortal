using Asp.Versioning;
using CareerWay.CompanyService.Application.Interfaces;
using CareerWay.CompanyService.Application.Models;
using CareerWay.Shared.AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.IdentityService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly ICompanyPackageService _companyPackageService;

    public CompaniesController(
        ICompanyService companyService,
        ICompanyPackageService companyPackageService)
    {
        _companyService = companyService;
        _companyPackageService = companyPackageService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        var company = await _companyService.GetDetail(id);
        return Ok(company);
    }

    [HttpGet("{id}/packages")]
    public async Task<IActionResult> GetPackages([FromRoute] long id, [FromQuery] bool? isUsed = null)
    {
        var packages = await _companyPackageService.GetCompanyPackages(id, isUsed);
        return Ok(packages);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanyRequest request)
    {
        await _companyService.Create(request);
        return Created("", new SuccessApiResponse());
    }
}
