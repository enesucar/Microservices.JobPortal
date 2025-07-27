using Asp.Versioning;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.JobSeekerService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class JobSeekerSkillsController : ControllerBase
{
    private readonly IJobSeekerSkillService _jobSeekerSkillService;

    public JobSeekerSkillsController(IJobSeekerSkillService jobSeekerSkillService)
    {
        _jobSeekerSkillService = jobSeekerSkillService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJobSeekerSkillRequest request)
    {
        await _jobSeekerSkillService.Create(request);
        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _jobSeekerSkillService.Delete(id);
        return NoContent();
    }
}

