using CareerWay.Shared.DynamicProxy;

namespace CareerWay.Shared.Authorization;

public class AuthorizeAttribute : BaseInterceptionAttribute
{
    public string[] Roles { get; set; }

    public AuthorizeAttribute(string[]? roles = null)
    {
        Roles = roles ?? [];
    }
}
