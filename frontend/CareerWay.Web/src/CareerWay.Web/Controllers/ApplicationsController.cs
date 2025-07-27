using CareerWay.Web.Interfaces;
using CareerWay.Web.Models.Applications;
using CareerWay.Web.Models.Post;
using Microsoft.AspNetCore.Mvc;
namespace CareerWay.Web.Controllers;

public class ApplicationsController : Controller
{
    private readonly IApplicationClient _applicationClient;
    private readonly ISearchClient _searchClient;
    private readonly IUser _user;

    public ApplicationsController(IApplicationClient applicationClient, IUser user, ISearchClient searchClient)
    {
        _applicationClient = applicationClient;
        _user = user;
        _searchClient = searchClient;
    }

    [HttpGet("is-ilanlarim/{id}/basvuranlar")]
    public async Task<IActionResult> Applicants([FromRoute] long id)
    {
        var applicants = await _applicationClient.GetList(id);
        return View(applicants);
    }

    [HttpGet("basvur/{slug}/{id}")]
    public async Task<IActionResult> Apply([FromRoute] string slug, [FromRoute] long id)
    {
        Application application = new Application();
        application.JobAdvertId = id;
        application.CreationDate = DateTime.Now;
        application.JobSeekerId = _user.Id;

        await _applicationClient.Apply(application);
        return RedirectToAction("Detail", "Posts", new { slug = slug, postId = id });
    }

    [HttpGet("basvuruyu-geri-al/{slug}/{id}")]
    public async Task<IActionResult> Withdraw([FromRoute] string slug, [FromRoute] long id)
    {
        await _applicationClient.Withdraw(id);
        return RedirectToAction("Detail", "Posts", new { slug = slug, postId = id });
    }

    [HttpGet("is-basvurularim")]
    public async Task<IActionResult> Index()
    {
        PostSearchRequest searchRequest = new PostSearchRequest();
        searchRequest.Data = await _searchClient.Applications(_user.Id);
        return View(searchRequest);
    }
}
