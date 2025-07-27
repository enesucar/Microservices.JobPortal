using Asp.Versioning;
using CareerWay.JobAdvertService.Application.Features.Cities.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.Cities.Commands.Edit;
using CareerWay.JobAdvertService.Application.Features.Cities.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.JobAdvertService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class CitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var cities = await _mediator.Send(new GetCityListQuery());
        return Ok(cities);
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody] CreateCityCommand command)
    {
        var result = await _mediator.Send(command);
        return Created(string.Empty, result);
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] EditCityCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
