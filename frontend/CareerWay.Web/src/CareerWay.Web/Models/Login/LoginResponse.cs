using CareerWay.Shared.Security.Tokens;

namespace CareerWay.Web.Models.Login;

public class LoginResponse : BaseApiResponse<LoginSuccessResponse, LoginErrorResponse>
{
    public bool Succeeded { get; set; }

    public bool Failed { get; set; }

    public bool IsLockedOut { get; set; }

    public bool RequiresTwoFactor { get; set; }

    public bool RequiresEmailConfirmation { get; set; }

    public AccessToken? AccessToken { get; set; }
}
