using CareerWay.Web.Consts;
using CareerWay.Web.Interfaces;
using CareerWay.Web.Models.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.Web.Controllers;

public class CompaniesController : Controller
{
    private readonly ISearchClient _searchClient;
    private readonly ICompanyClient _companyClient;
    private readonly IUser _user;

    public CompaniesController(
        ISearchClient searchClient,
        IUser user,
        ICompanyClient companyClient)
    {
        _searchClient = searchClient;
        _user = user;
        _companyClient = companyClient;
    }

    [Authorize(AuthenticationSchemes = SecurityConsts.CompanyAuthenticationSchemeName)]
    [HttpGet("is-ilanlarim")]
    public async Task<IActionResult> Posts()
    {
        PostSearchRequest searchRequest = new PostSearchRequest()
        {
            CompanyId = _user.Id
        };

        searchRequest.Data = await _searchClient.Search(searchRequest);
        return View(searchRequest);
    }

    [HttpGet("is-verenler/{id}")]
    public async Task<IActionResult> Detail(long id)
    { 
        var model = await _companyClient.GetCompanyDetail(id);
        return View(model);
    }

    [HttpGet("is-paketlerim")]
    public async Task<IActionResult> PackageList()
    {
        var model = await _companyClient.GetPackages(_user.Id);
        return View(model);
    }
}
