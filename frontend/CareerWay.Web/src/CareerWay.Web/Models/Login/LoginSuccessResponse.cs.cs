using CareerWay.Shared.Security.Tokens;

namespace CareerWay.Web.Models.Login;

public class LoginSuccessResponse
{
    public bool Succeeded { get; set; }

    public AccessToken? AccessToken { get; set; }
}
