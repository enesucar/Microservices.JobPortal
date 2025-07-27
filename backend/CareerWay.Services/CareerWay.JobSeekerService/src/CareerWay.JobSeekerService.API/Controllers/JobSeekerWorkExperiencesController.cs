using Asp.Versioning;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.JobSeekerService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class JobSeekerWorkExperiencesController : ControllerBase
{
    private readonly IJobSeekerWorkExperienceService _jobSeekerWorkExperienceService;

    public JobSeekerWorkExperiencesController(IJobSeekerWorkExperienceService jobSeekerWorkExperienceService)
    {
        _jobSeekerWorkExperienceService = jobSeekerWorkExperienceService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJobSeekerWorkExperienceRequest request)
    {
        await _jobSeekerWorkExperienceService.Create(request);
        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _jobSeekerWorkExperienceService.Delete(id);
        return NoContent();
    }
}

