using Asp.Versioning;
using CareerWay.JobAdvertService.Application.Features.Sectors.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.Sectors.Commands.Edit;
using CareerWay.JobAdvertService.Application.Features.Sectors.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.JobAdvertService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class SectorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SectorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var sectors = await _mediator.Send(new GetSectorListQuery());
        return Ok(sectors);
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody] CreateSectorCommand command)
    {
        var result = await _mediator.Send(command);
        return Created(string.Empty, result);
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] EditSectorCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
