namespace CareerWay.Shared.MediatR.Behaviours.Authorization;

public interface ISecuredRequest
{
    string[] Roles { get; }
}
