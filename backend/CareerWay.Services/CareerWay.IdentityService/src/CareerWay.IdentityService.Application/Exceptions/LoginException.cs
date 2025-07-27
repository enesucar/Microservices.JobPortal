using CareerWay.IdentityService.Application.Models;
using CareerWay.Shared.Core.Exceptions;

namespace CareerWay.IdentityService.Application.Exceptions;

public class LoginException : BusinessException
{
    public LoginException(LoginResponse response)
    {
        Title = "Login Error";
        Errors = response;
    }
}
