using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.DynamicProxy;
using CareerWay.Shared.Security.Users;
using System.Reflection;

namespace CareerWay.Shared.Authorization;

public class AuthorizeInterception : MethodInterceptionBase
{
    private readonly ICurrentUser _currentUser;

    public AuthorizeInterception(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    protected override Task OnEntryAsync(IMethodInvocation methodInvocation)
    {
        var authorizeAttribute = methodInvocation.MethodInvocationTarget.GetCustomAttributes<AuthorizeAttribute>(true).FirstOrDefault();
        if (authorizeAttribute == null)
        {
            return Task.CompletedTask;
        }

        if (!_currentUser.IsAuthenticated)
        {
            throw new UnauthorizedAccessException();
        }

        if (authorizeAttribute.Roles.Any() && !_currentUser.IsInRoles(authorizeAttribute.Roles))
        {
            throw new ForbiddenAccessException();
        }

        return Task.CompletedTask;
    }
}
