using Asp.Versioning;
using CareerWay.JobAdvertService.Application.Features.PostEducationLevels.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.PostEducationLevels.Queries.GetList;
using CareerWay.JobAdvertService.Application.Features.PostLanguageRequirements.Commands;
using CareerWay.JobAdvertService.Application.Features.PostLanguageRequirements.Queries.GetList;
using CareerWay.JobAdvertService.Application.Features.Posts.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.Posts.Commands.Publish;
using CareerWay.JobAdvertService.Application.Features.Posts.Queries.GetById;
using CareerWay.JobAdvertService.Application.Features.PostSectors.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.PostSectors.Queries.GetList;
using CareerWay.JobAdvertService.Application.Features.PostWorkBenefits.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.PostWorkBenefits.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.JobAdvertService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        var result = await _mediator.Send(new GetPostDetailQuery() { Id = id });
        return Created(string.Empty, result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePostCommand command)
    {
        var result = await _mediator.Send(command);
        return Created(string.Empty, result);
    }

    [HttpGet("{id}/education-levels")]
    public async Task<IActionResult> GetEducationLevels([FromRoute] long id)
    {
        var result = await _mediator.Send(new GetPostEducationListQuery() { PostId = id });
        return Created(string.Empty, result);
    }

    [HttpPost("{id}/education-levels")]
    public async Task<IActionResult> CreateEducationLevels([FromRoute] long id, [FromBody] CreatePostEducationLevelCommand command)
    {
        command.PostId = id;
        var result = await _mediator.Send(command);
        return Created(string.Empty, result);
    }

    [HttpGet("{id}/language-requirements")]
    public async Task<IActionResult> GetLanguageRequirements([FromRoute] long id)
    {
        var result = await _mediator.Send(new GetPostLanguageRequirementListQuery() { PostId = id });
        return Created(string.Empty, result);
    }


    [HttpPost("{id}/language-requirements")]
    public async Task<IActionResult> CreateLanguageRequirements([FromRoute] long id, [FromBody] CreatePostLanguageRequirementCommand command)
    {
        command.PostId = id;
        var result = await _mediator.Send(command);
        return Created(string.Empty, result);
    }


    [HttpGet("{id}/work-benefits")]
    public async Task<IActionResult> GetWorkBenefits([FromRoute] long id)
    {
        var result = await _mediator.Send(new GetPostWorkBenefitListQuery() { PostId = id });
        return Created(string.Empty, result);
    }


    [HttpPost("{id}/work-benefits")]
    public async Task<IActionResult> CreateWorkBenefits([FromRoute] long id, [FromBody] CreateWorkBenefitCommand command)
    {
        command.PostId = id;
        var result = await _mediator.Send(command);
        return Created(string.Empty, result);
    }

    [HttpGet("{id}/sectors")]
    public async Task<IActionResult> GetPostSectors([FromRoute] long id)
    {
        var result = await _mediator.Send(new GetPostSectorListQuery() { PostId = id });
        return Created(string.Empty, result);
    }


    [HttpPost("{id}/sectors")]
    public async Task<IActionResult> CreatePostSectors([FromRoute] long id, [FromBody] CreatePostSectorCommand command)
    {
        command.PostId = id;
        var result = await _mediator.Send(command);
        return Created(string.Empty, result);
    }

    [HttpPost("{id}/publish")]
    public async Task<IActionResult> Publish([FromRoute] long id)
    {
        var result = await _mediator.Send(new PublishPostCommand() { Id = id });
        return Created(string.Empty, result);
    }
}