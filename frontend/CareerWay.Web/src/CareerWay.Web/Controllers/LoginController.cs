using CareerWay.Web.Interfaces;
using CareerWay.Web.Models.Login;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.Web.Controllers;

public class LoginController : Controller
{
    private readonly IIdentityClient _userClient;

    public LoginController(IIdentityClient userClient)
    {
        _userClient = userClient;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginRequest request)
    {
        var loginResponse = await _userClient.LoginAsync(request);
        if (loginResponse.IsSuccess)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = loginResponse.AccessToken!.Expiration;
            options.HttpOnly = true;
            Response.Cookies.Append("token", loginResponse.AccessToken!.Token, options);
            return Redirect("/");
        }
        else if (loginResponse.Failed)
        {
            ViewData["Errors"] = "Email veya şifre yanlış";
        }
        else if (loginResponse.RequiresEmailConfirmation)
        {
            ViewData["Errors"] = "Email oynaylanmamış.";
        }
        else if (loginResponse.IsLockedOut)
        {
            ViewData["Errors"] = "Çok fazla deneme. Daha sonra tekrar deneyiniz.";
        }
        else if (loginResponse.RequiresTwoFactor)
        {
            //two factor
        }
        else
        {
            ViewData["Errors"] = "Bir şeyler yanlış gitti. Daha sonra tekrar deneyiniz.";
        }

        return View();
    }
}
