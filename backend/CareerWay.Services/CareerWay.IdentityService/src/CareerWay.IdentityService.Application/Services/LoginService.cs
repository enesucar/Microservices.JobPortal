using CareerWay.IdentityService.Application.Exceptions;
using CareerWay.IdentityService.Application.Interfaces;
using CareerWay.IdentityService.Application.Models;
using CareerWay.IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CareerWay.IdentityService.Application.Services;

public class LoginService : ILoginService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public LoginService(
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return LoginResponse.Fail();
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
        if (signInResult.Succeeded)
        {
            var role = await _userManager.GetRolesAsync(user);
            return LoginResponse.Success(user.Id, role.First());
        }

        throw new LoginException((LoginResponse)signInResult);
    }
}
