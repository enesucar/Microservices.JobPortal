namespace CareerWay.AuthenticationServer.Web.Models;

public class LoginResponse
{
    public long? UserId { get; set; }

    public string? Role { get; set; }

    public bool Succeeded { get; set; }

    public bool Failed { get; set; }

    public bool IsLockedOut { get; set; }

    public bool RequiresTwoFactor { get; set; }

    public bool RequiresEmailConfirmation { get; set; }
}
