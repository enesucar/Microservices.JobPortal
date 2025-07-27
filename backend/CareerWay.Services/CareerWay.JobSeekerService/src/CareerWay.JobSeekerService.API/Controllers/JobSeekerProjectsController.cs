using Asp.Versioning;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.JobSeekerService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class JobSeekerProjectsController : ControllerBase
{
    private readonly IJobSeekerProjectService _jobSeekerProjectService;

    public JobSeekerProjectsController(IJobSeekerProjectService jobSeekerProjectService)
    {
        _jobSeekerProjectService = jobSeekerProjectService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJobSeekerProjectRequest request)
    {
        await _jobSeekerProjectService.Create(request);
        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _jobSeekerProjectService.Delete(id);
        return NoContent();
    }
}

