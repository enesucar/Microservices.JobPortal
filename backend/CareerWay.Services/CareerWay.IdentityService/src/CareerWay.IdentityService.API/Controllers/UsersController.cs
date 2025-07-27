using Asp.Versioning;
using CareerWay.IdentityService.Application.Interfaces;
using CareerWay.IdentityService.Application.Models;
using CareerWay.Shared.AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.IdentityService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
    {
        var user = await _userService.Register(request);
        var response = new SuccessApiResponse<CreateUserResponse>(user)
        {
            Data = user
        };
        return Created("", response);
    }
}

