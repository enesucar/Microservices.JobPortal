using CareerWay.Web.Enums;
using CareerWay.Web.Helpers;
using CareerWay.Web.Interfaces;
using CareerWay.Web.Models.JobSeekers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CareerWay.Web.Controllers;

public class JobSeekersController : Controller
{
    private readonly IJobSeekerClient _jobSeekerClient;
    private readonly IUser _user;
    private readonly IJobAdvertClient _jobAdvertClient;
    private readonly IMediaClient _mediaClient;

    public JobSeekersController(IJobSeekerClient jobSeekerClient, IUser user, IJobAdvertClient jobAdvertClient, IMediaClient mediaClient)
    {
        _jobSeekerClient = jobSeekerClient;
        _user = user;
        _jobAdvertClient = jobAdvertClient;
        _mediaClient = mediaClient;
    }

    [HttpGet("oz-gecmisim")]
    public async Task<IActionResult> Index()
    {
        var jobSeeker = await _jobSeekerClient.GetDetail(_user.Id);
        return View(jobSeeker);
    }

    [HttpGet("oz-gecmis-goruntule/{jobSeekerId}")]
    public async Task<IActionResult> Index(long jobSeekerId)
    {
        var jobSeeker = await _jobSeekerClient.GetDetail(jobSeekerId);
        return View(jobSeeker);
    }

    [HttpGet("oz-gecmis-duzenle/kisisel-bilgilerim")]
    public async Task<IActionResult> EditPersonalInformation()
    {
        var jobSeeker = await _jobSeekerClient.GetDetail(_user.Id);
        var editJobSeekerRequest = new EditJobSeekerRequest()
        {
            AboutMe = jobSeeker.AboutMe,
            Address = jobSeeker.Address,
            BirthDate = jobSeeker.BirthDate,
            CityId = jobSeeker.City?.Id,
            FirstName = jobSeeker.FirstName,
            Id = _user.Id,
            DriverLicenseType = jobSeeker.DriverLicenseType,
            Facebook = jobSeeker.Facebook,
            GenderType = jobSeeker.GenderType,
            Github = jobSeeker.Github,
            Instragram = jobSeeker.Instragram,
            Interests = jobSeeker.Interests,
            IsSmoking = jobSeeker.IsSmoking,
            LastName = jobSeeker.LastName,
            Linkedin = jobSeeker.Linkedin,
            MilitaryStatus = jobSeeker.MilitaryStatus,
            PhoneNumber = jobSeeker.PhoneNumber,
            ProfilePhoto = jobSeeker.ProfilePhoto,
            Twitter = jobSeeker.Twitter,
            WebSite = jobSeeker.WebSite
        };
        return View(editJobSeekerRequest);
    }

    [HttpPost("oz-gecmis-duzenle/kisisel-bilgilerim")]
    public async Task<IActionResult> EditPersonalInformation([FromForm] EditJobSeekerRequest request, IFormFile? profilePhotoFile)
    {
        if (profilePhotoFile != null)
        {
            var file = await _mediaClient.Create(profilePhotoFile);
            request.ProfilePhoto = file;
        }
        request.Id = _user.Id;
        await _jobSeekerClient.Edit(request);
        return RedirectToAction("EditSchool");
    }


    [HttpGet("oz-gecmis-duzenle/okul-bilgilerim")]
    public async Task<IActionResult> EditSchool()
    {
        var jobSeeker = await _jobSeekerClient.GetDetail(_user.Id);
        ViewBag.EducationLevelTypes = EnumHelper.ToSelectList<EducationLevelType>();
        var schools = jobSeeker.JobSeekerSchools.Select(o => new CreateJobSeekerSchoolRequest()
        {
            EducationLevelType = o.EducationLevelType,
            EndYear = o.EndYear,
            Name = o.Name,
            StartYear = o.StartYear
        }).ToList();
        var model = new CreateJobSeekerSchoolViewModel()
        {
            Schools = schools
        };
        return View(model);
    }

    [HttpPost("oz-gecmis-duzenle/okul-bilgilerim")]
    public async Task<IActionResult> EditSchool(CreateJobSeekerSchoolViewModel model)
    {
        await _jobSeekerClient.CreateSchools(model.Schools);
        return RedirectToAction("EditReferences");
    }

    [HttpGet("oz-gecmis-duzenle/referanslarim")]
    public async Task<IActionResult> EditReference()
    {
        var jobSeeker = await _jobSeekerClient.GetDetail(_user.Id);
        var positions = await _jobAdvertClient.GetPositions();
        var references = jobSeeker.JobSeekerReferences.Select(o => new CreateJobSeekerReferenceRequest()
        {
            CompanyName = o.CompanyName,
            FullName = o.FullName,
            PhoneNumber = o.PhoneNumber,
            PositionId = o.Position?.Id
        }).ToList();
        ViewBag.Positions = positions.Items.Select(p => new SelectListItem
        {
            Value = p.Id.ToString(),
            Text = p.Name
        }).ToList();
        var model = new CreateJobSeekerReferenceViewModel()
        {
            References = references
        };
        return View(model);
    }


    [HttpPost("oz-gecmis-duzenle/referanslarim")]
    public async Task<IActionResult> EditReference(CreateJobSeekerReferenceViewModel model)
    {
        await _jobSeekerClient.CreateReferences(model.References);
        return RedirectToAction("EditSchool");
    }
}
