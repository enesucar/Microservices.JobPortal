using CareerWay.IdentityService.Application.Exceptions;
using CareerWay.IdentityService.Application.Interfaces;
using CareerWay.IdentityService.Application.Models;
using CareerWay.IdentityService.Domain.Entities;
using CareerWay.Shared.SnowflakeId;
using CareerWay.Shared.TimeProviders;
using Microsoft.AspNetCore.Identity;

namespace CareerWay.IdentityService.Application.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IDateTime _dateTime;
    private readonly ISnowflakeIdGenerator _snowflakeIdGenerator;

    public UserService(
        UserManager<User> userManager,
        IDateTime dateTime,
        ISnowflakeIdGenerator snowflakeIdGenerator)
    {
        _userManager = userManager;
        _dateTime = dateTime;
        _snowflakeIdGenerator = snowflakeIdGenerator;
    }

    public async Task<CreateUserResponse> Register(CreateUserRequest request)
    {
        var user = new User()
        {
            Id = _snowflakeIdGenerator.Generate(),
            Email = request.Email,
            CreationDate = _dateTime.Now,
            LastLoginDate = _dateTime.Now,
        };

        var createUserResult = await _userManager.CreateAsync(user, request.Password);
        if (!createUserResult.Succeeded)
        {
            throw new RegisterException(createUserResult.Errors);
        }

        var addToRoleResult = await _userManager.AddToRoleAsync(user, request.Role);
        if (!createUserResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);
            throw new RegisterException(addToRoleResult.Errors);
        }

        return new CreateUserResponse()
        {
            Id = user.Id,
            CreationDate = user.CreationDate
        };
    }
}
