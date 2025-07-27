using Asp.Versioning;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.JobSeekerService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class JobSeekerSchoolsController : ControllerBase
{
    private readonly IJobSeekerSchoolService _jobSeekerSchoolService;

    public JobSeekerSchoolsController(IJobSeekerSchoolService jobSeekerSchoolService)
    {
        _jobSeekerSchoolService = jobSeekerSchoolService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] List<CreateJobSeekerSchoolRequest> request)
    {
        await _jobSeekerSchoolService.Create(request);
        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _jobSeekerSchoolService.Delete(id);
        return NoContent();
    }
}

