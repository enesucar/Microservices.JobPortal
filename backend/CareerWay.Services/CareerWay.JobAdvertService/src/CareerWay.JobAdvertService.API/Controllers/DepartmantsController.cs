using Asp.Versioning;
using CareerWay.JobAdvertService.Application.Features.Departmants.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.Departmants.Commands.Edit;
using CareerWay.JobAdvertService.Application.Features.Departmants.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.JobAdvertService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class DepartmantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var departmants = await _mediator.Send(new GetDepartmantListQuery());
        return Ok(departmants);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepartmantCommand command)
    {
        var result = await _mediator.Send(command);
        return Created(string.Empty, result);
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] EditDepartmantCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
