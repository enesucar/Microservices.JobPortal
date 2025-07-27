using CareerWay.Shared.Core.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace CareerWay.IdentityService.Application.Exceptions;

public class RegisterException : BusinessException
{
    public RegisterException(IEnumerable<IdentityError> errors)
    {
        Errors = errors;
        Title = "Register Error";
    }
}
