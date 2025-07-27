using Asp.Versioning;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.JobSeekerService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class JobSeekerReferencesController : ControllerBase
{
    private readonly IJobSeekerReferenceService _jobSeekerReferenceService;

    public JobSeekerReferencesController(IJobSeekerReferenceService jobSeekerReferenceService)
    {
        _jobSeekerReferenceService = jobSeekerReferenceService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] List<CreateJobSeekerReferenceRequest> request)
    {
        await _jobSeekerReferenceService.Create(request);
        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _jobSeekerReferenceService.Delete(id);
        return NoContent();
    }
}
