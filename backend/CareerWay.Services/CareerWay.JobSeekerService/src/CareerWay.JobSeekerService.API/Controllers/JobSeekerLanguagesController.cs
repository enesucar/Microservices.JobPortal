using Asp.Versioning;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.JobSeekerService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class JobSeekerLanguagesController : ControllerBase
{
    private readonly IJobSeekerLanguageService _jobSeekerLanguageService;

    public JobSeekerLanguagesController(IJobSeekerLanguageService jobSeekerLanguageService)
    {
        _jobSeekerLanguageService = jobSeekerLanguageService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJobSeekerLanguageRequest request)
    {
        await _jobSeekerLanguageService.Create(request);
        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _jobSeekerLanguageService.Delete(id);
        return NoContent();
    }
}
