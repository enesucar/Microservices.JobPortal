using Asp.Versioning;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.JobSeekerService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class JobSeekerCertificatesController : ControllerBase
{
    private readonly IJobSeekerCertificateService _jobSeekerCertificateService;

    public JobSeekerCertificatesController(IJobSeekerCertificateService jobSeekerCertificateService)
    {
        _jobSeekerCertificateService = jobSeekerCertificateService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJobSeekerCertificateRequest request)
    {
        await _jobSeekerCertificateService.Create(request);
        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _jobSeekerCertificateService.Delete(id);
        return NoContent();
    }
}
