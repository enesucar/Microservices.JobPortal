using Asp.Versioning;
using CareerWay.IdentityService.Application.Interfaces;
using CareerWay.IdentityService.Application.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _loginService.Login(request);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
