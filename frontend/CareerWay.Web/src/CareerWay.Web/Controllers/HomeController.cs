using CareerWay.Web.Consts;
using CareerWay.Web.Enums;
using CareerWay.Web.Helpers;
using CareerWay.Web.Interfaces;
using CareerWay.Web.Models;
using CareerWay.Web.Models.Payment;
using CareerWay.Web.Models.Post;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Reflection;
using Web.Domain.Enums;

namespace CareerWay.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPaymentClient _paymentClient;
    private readonly ICompanyClient _companyClient;
    private readonly IJobAdvertClient _jobAdvertClient;
    private readonly IUser _user;
    private readonly ISearchClient _searchClient;

    public HomeController(
        ILogger<HomeController> logger,
        IPaymentClient paymentClient,
        ICompanyClient companyClient,
        IUser user,
        IJobAdvertClient jobAdvertClient,
        ISearchClient searchClient)
    {
        _logger = logger;
        _paymentClient = paymentClient;
        _companyClient = companyClient;
        _user = user;
        _jobAdvertClient = jobAdvertClient;
        _searchClient = searchClient;
    }

    public async Task<IActionResult> Index()
    {
        PostSearchRequest searchRequest = new PostSearchRequest();
        searchRequest.PostStatus = PostStatus.Approved;
        searchRequest.UserId = _user.IsAuthenticated ? _user.Id : null;

        var departmants = await _jobAdvertClient.GetDepartmants();
        var positions = await _jobAdvertClient.GetPositions();
        var cities = await _jobAdvertClient.GetCities();

        ViewBag.WorkingTypes = EnumHelper.ToSelectList<WorkingType>();
        ViewBag.PositionLevelTypes = EnumHelper.ToSelectList<PositionLevelType>();
        ViewBag.ExperienceTypes = EnumHelper.ToSelectList<ExperienceType>();
        ViewBag.Departments = departmants.Items;
        ViewBag.Positions = positions.Items;
        ViewBag.Cities = new SelectList(cities.Items, "Id", "Name");

        if (_user.IsAuthenticated)
        {
            searchRequest.UserId = _user.Id;
        }

        searchRequest.Data = await _searchClient.Search(searchRequest);
        return View("Views/Posts/Index.cshtml", searchRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Index(PostSearchRequest postSearchRequest)
    {
        var departmants = await _jobAdvertClient.GetDepartmants();
        var positions = await _jobAdvertClient.GetPositions();
        var cities = await _jobAdvertClient.GetCities();
        postSearchRequest.PostStatus = PostStatus.Approved;
        ViewBag.WorkingTypes = EnumHelper.ToSelectList<WorkingType>();
        ViewBag.PositionLevelTypes = EnumHelper.ToSelectList<PositionLevelType>();
        ViewBag.ExperienceTypes = EnumHelper.ToSelectList<ExperienceType>();
        ViewBag.Departments = departmants.Items;
        ViewBag.Positions = positions.Items;
        ViewBag.Cities = new SelectList(cities.Items, "Id", "Name");

        postSearchRequest.Data = await _searchClient.Search(postSearchRequest);
        return View("Views/Posts/Index.cshtml", postSearchRequest);
    }

    //[Authorize(AuthenticationSchemes = SecurityConsts.CompanyAuthenticationSchemeName)]
    //public async Task<IActionResult> Index(long? id)
    //{
    //    var packages = await _companyClient.GetPackages(_user.Id);
    //    var departmants = await _jobAdvertClient.GetDepartmants();
    //    var positions = await _jobAdvertClient.GetPositions();
    //    var cities = await _jobAdvertClient.GetCities();

    //    var model = new CreatePostRequest();
    //    if (id != null)
    //    {
    //        var postDetail = await _jobAdvertClient.GetDetail(id.Value!);
    //        model.Title = postDetail.Title;
    //        model.Description = postDetail.Description;
    //        model.DriverLicenseType = postDetail.DriverLicenseType ?? DriverLicenseType.NoLicense;
    //        model.PackageId = postDetail.PackageId;
    //        model.WorkingType = postDetail.WorkingType;
    //        model.PositionLevelType = postDetail.PositionLevelType;
    //        model.GenderType = postDetail.GenderType;
    //        model.ExperienceType = postDetail.ExperienceType;
    //        model.DepartmantId = postDetail.Departmant.Id;
    //        model.PositionId = postDetail.Position.Id;
    //        model.CityId = postDetail.City.Id;
    //        model.PackageId = postDetail.PackageId;
    //        model.IsDisabledOnly = postDetail.IsDisabledOnly;
    //    }

    //    ViewBag.WorkingTypes = EnumHelper.ToSelectList<WorkingType>();
    //    ViewBag.PositionLevelTypes = EnumHelper.ToSelectList<PositionLevelType>();
    //    ViewBag.DriverLicenseTypes = EnumHelper.ToSelectList<DriverLicenseType>();
    //    ViewBag.ExperienceTypes = EnumHelper.ToSelectList<ExperienceType>();
    //    ViewBag.GenderTypes = EnumHelper.ToSelectList<GenderType>();
    //    ViewBag.Departments = new SelectList(departmants.Items, "Id", "Name");
    //    ViewBag.Positions = new SelectList(positions.Items, "Id", "Name");
    //    ViewBag.Cities = new SelectList(cities.Items, "Id", "Name");

    //    return View("Views/Posts/Create.cshtml", model);
    //} 


    [Authorize(AuthenticationSchemes = SecurityConsts.CompanyAuthenticationSchemeName)]
    [HttpPost]
    public async Task<IActionResult> Payment(CreatePaymentRequest request)
    {
        var result = await _paymentClient.Create(request);
        if (result)
        {
            return View("Views/Packages/PaymentSuccess.cshtml", request);
        }
        return View("Views/Packages/Payment.cshtml", request);
    }

    [HttpGet("cikis")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync(SecurityConsts.CompanyAuthenticationSchemeName);
        await HttpContext.SignOutAsync(SecurityConsts.JobSeekerAuthenticationSchemeName);
        return RedirectToAction("Index", "Home");
    }

    [Authorize(AuthenticationSchemes = SecurityConsts.CompanyAuthenticationSchemeName)]
    [HttpGet("is-veren-girisi")]
    public async Task<IActionResult> JobSeekerLogin()
    {
        return RedirectToAction("Index", "Home");
    }

    [Authorize(AuthenticationSchemes = SecurityConsts.JobSeekerAuthenticationSchemeName)]
    [HttpGet("is-arayan-girisi")]
    public async Task<IActionResult> CompanyLogin()
    {
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
