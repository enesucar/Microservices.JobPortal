using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.Security.Users;
using MediatR;

namespace CareerWay.Shared.MediatR.Behaviours.Authorization;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly ICurrentUser _currentUser;

    public AuthorizationBehaviour(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated)
        {
            throw new UnauthorizedAccessException();
        }

        if (request.Roles.Any() && !_currentUser.IsInRoles(request.Roles))
        {
            throw new ForbiddenAccessException();
        }

        return await next();
    }
}
