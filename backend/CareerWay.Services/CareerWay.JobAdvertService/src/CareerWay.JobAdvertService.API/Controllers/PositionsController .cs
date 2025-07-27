using Asp.Versioning;
using CareerWay.JobAdvertService.Application.Features.Positions.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.Positions.Commands.Edit;
using CareerWay.JobAdvertService.Application.Features.Positions.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.JobAdvertService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class PositionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PositionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var positions = await _mediator.Send(new GetPositionListQuery());
        return Ok(positions);
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody] CreatePositionCommand command)
    {
        var result = await _mediator.Send(command);
        return Created(string.Empty, result);
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] EditPositionCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
