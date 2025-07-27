using Asp.Versioning;
using CareerWay.JobAdvertService.Application.Features.Skills.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.Skills.Commands.Edit;
using CareerWay.JobAdvertService.Application.Features.Skills.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.JobAdvertService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class SkillsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SkillsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var skills = await _mediator.Send(new GetSkillListQuery());
        return Ok(skills);
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody] CreateSkillCommand command)
    {
        var result = await _mediator.Send(command);
        return Created(string.Empty, result);
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] EditSkillCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
