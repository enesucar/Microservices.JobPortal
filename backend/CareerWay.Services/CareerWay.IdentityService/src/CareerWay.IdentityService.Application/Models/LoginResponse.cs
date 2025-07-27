using Microsoft.AspNetCore.Identity;

namespace CareerWay.IdentityService.Application.Models;

public class LoginResponse
{
    public long? UserId { get; set; }

    public string? Role { get; set; }

    public bool Succeeded { get; set; }

    public bool Failed { get; set; }

    public bool IsLockedOut { get; set; }

    public bool RequiresTwoFactor { get; set; }

    public bool RequiresEmailConfirmation { get; set; }

    private LoginResponse()
    {
    }

    public static LoginResponse Success(long userId, string role)
    {
        return new LoginResponse() { Succeeded = true, UserId = userId, Role = role };
    }

    public static LoginResponse Fail()
    {
        return new LoginResponse() { Failed = true };
    }

    public static LoginResponse LockedOut()
    {
        return new LoginResponse() { IsLockedOut = true };
    }

    public static LoginResponse TwoFactorRequired()
    {
        return new LoginResponse() { RequiresTwoFactor = true };
    }

    public static LoginResponse EmailConfirmationRequired()
    {
        return new LoginResponse() { RequiresEmailConfirmation = true };
    }

    public static explicit operator LoginResponse(SignInResult signInResult)
    {
        if (signInResult.Succeeded)
        {
            return new LoginResponse() { Succeeded = true };
        }
        else if (signInResult.IsNotAllowed)
        {
            return EmailConfirmationRequired();
        }
        else if (signInResult.IsLockedOut)
        {
            return LockedOut();
        }
        else if (signInResult.RequiresTwoFactor)
        {
            return TwoFactorRequired();
        }
        else
        {
            return Fail();
        }
    }
}
