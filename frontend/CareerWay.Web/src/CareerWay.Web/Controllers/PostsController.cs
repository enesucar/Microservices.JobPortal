using CareerWay.Web.Consts;
using CareerWay.Web.Enums;
using CareerWay.Web.Helpers;
using CareerWay.Web.Interfaces;
using CareerWay.Web.Models.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Domain.Enums;

namespace CareerWay.Web.Controllers;

public class PostsController : Controller
{
    private readonly IJobAdvertClient _jobAdvertClient;
    private readonly ICompanyClient _companyClient;
    private readonly IUser _user;
    private readonly IApplicationClient _applicationClient;
    private readonly ISearchClient _searchClient;

    public PostsController(
        IJobAdvertClient jobAdvertClient,
        IUser user,
        ICompanyClient companyClient,
        IApplicationClient applicationClient,
        ISearchClient searchClient)
    {
        _jobAdvertClient = jobAdvertClient;
        _user = user;
        _companyClient = companyClient;
        _applicationClient = applicationClient;
        _searchClient = searchClient;
    }

    [HttpGet("is-ilanlari/{slug}-{postId}")]
    public async Task<IActionResult> Detail([FromRoute] string slug, [FromRoute] long postId)
    {
        var applicationCount = await _applicationClient.GetCount(postId);
        var post = await _jobAdvertClient.GetDetail(postId);
        var relatedPosts = await _searchClient.RelatedPosts(postId);
        var model = new PostDetailViewModel()
        {
            PostDetail = post,
            ApplicationCount = applicationCount,
            IsApplied = false,
            RelatedJobs = relatedPosts
        };

        if (_user.IsAuthenticated)
        {
            model.IsApplied = await _applicationClient.IsApplied(postId);
        }

        return View(model);
    }

    [Authorize(AuthenticationSchemes = SecurityConsts.CompanyAuthenticationSchemeName)]
    [HttpGet("is-ilani-olustur")]
    public async Task<IActionResult> Create(long? id)
    {
        var model = new CreatePostRequest();
        var packages = await _companyClient.GetPackages(_user.Id, false);

        if (packages.Count == 0)
        {
            model.PackageId = -1;
            return View(model);
        }

        var packageNames = new Dictionary<int, string>
        {
            { 1, "Basit Paket" },
            { 2, "Standart Paket" },
            { 3, "Platinyum Paket" }
        };

        var departmants = await _jobAdvertClient.GetDepartmants();
        var positions = await _jobAdvertClient.GetPositions();
        var cities = await _jobAdvertClient.GetCities();
        var selectListItems = packages.Select(p => new SelectListItem
        {
            Value = p.Id.ToString(),
            Text = $"{p.Id} - {packageNames.GetValueOrDefault(p.PackageId, "Bilinmeyen Paket")} - {p.CreationDate:dd.MM.yyyy}"
        }).ToList();

        if (id != null)
        {
            var postDetail = await _jobAdvertClient.GetDetail(id.Value!);
            model.Title = postDetail.Title;
            model.Description = postDetail.Description;
            model.DriverLicenseType = postDetail.DriverLicenseType ?? DriverLicenseType.NoLicense;
            model.PackageId = postDetail.PackageId;
            model.WorkingType = postDetail.WorkingType;
            model.PositionLevelType = postDetail.PositionLevelType;
            model.GenderType = postDetail.GenderType;
            model.ExperienceType = postDetail.ExperienceType;
            model.DepartmantId = postDetail.Departmant.Id;
            model.PositionId = postDetail.Position.Id;
            model.CityId = postDetail.City.Id;
            model.PackageId = postDetail.PackageId;
            model.IsDisabledOnly = postDetail.IsDisabledOnly;
        }

        ViewBag.Packages = selectListItems;
        ViewBag.WorkingTypes = EnumHelper.ToSelectList<WorkingType>();
        ViewBag.PositionLevelTypes = EnumHelper.ToSelectList<PositionLevelType>();
        ViewBag.DriverLicenseTypes = EnumHelper.ToSelectList<DriverLicenseType>();
        ViewBag.ExperienceTypes = EnumHelper.ToSelectList<ExperienceType>();
        ViewBag.GenderTypes = EnumHelper.ToSelectList<GenderType>();
        ViewBag.Departments = new SelectList(departmants.Items, "Id", "Name");
        ViewBag.Positions = new SelectList(positions.Items, "Id", "Name");
        ViewBag.Cities = new SelectList(cities.Items, "Id", "Name");

        return View(model);
    }

    [Authorize(AuthenticationSchemes = SecurityConsts.CompanyAuthenticationSchemeName)]
    [HttpPost("is-ilani-olustur")]
    public async Task<IActionResult> CreatePost(CreatePostRequest request)
    {
        var packages = await _companyClient.GetPackages(_user.Id, false);
        request.PackageId = packages.Where(o => o.Id == request.CompanyPackageId).FirstOrDefault()!.PackageId; 
        await _jobAdvertClient.Create(request);
        return View(request);
    }

    [HttpGet("ilan-yayinla/{id}")]
    public async Task<IActionResult> Publish(long id)
    {
        await _jobAdvertClient.Publish(new() { Id = id });
        return RedirectToAction("Posts", "Companies");
    }
}
