using CareerWay.AuthenticationServer.Web.Interfaces;
using CareerWay.AuthenticationServer.Web.Models;
using CareerWay.AuthenticationServer.Web.Pages.Account.Login;
using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CareerWay.AuthenticationServer.Web.Pages.JobSeeker;

[AllowAnonymous]
public class RegisterModel : PageModel
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IClientStore _clientStore;
    private readonly IAuthenticationSchemeProvider _schemeProvider;
    private readonly IIdentityProviderStore _identityProviderStore;
    private readonly IRegistrationClient _registrationClient;

    public RegisterModel(IIdentityServerInteractionService interaction, IClientStore clientStore, IAuthenticationSchemeProvider schemeProvider, IIdentityProviderStore identityProviderStore, IRegistrationClient registrationClient)
    {
        _interaction = interaction;
        _clientStore = clientStore;
        _schemeProvider = schemeProvider;
        _identityProviderStore = identityProviderStore;
        _registrationClient = registrationClient;
    }

    [BindProperty]
    public InputModel Input { get; set; } = default!;

    public async Task<IActionResult> OnGet(string returnUrl)
    {
        Input = new InputModel
        {
            ReturnUrl = returnUrl
        };
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);
        if (ModelState.IsValid)
        {
            var response = await _registrationClient.Create(new CreateJobSeekerRequest()
            {
                Email = Input.Email,
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                Password = Input.Password
            });

            if (response.Success)
            {
                AuthenticationProperties? props = null;
                var successResponse = (SuccessResponse<CreateUserResponse>)response;

                var isuser = new IdentityServerUser(successResponse.Data!.Id.ToString());
                isuser.AdditionalClaims.Add(new Claim(ClaimTypes.Role, "JobSeeker"));
                isuser.AdditionalClaims.Add(new Claim(ClaimTypes.NameIdentifier, successResponse.Data.Id.ToString()!));

                await HttpContext.SignInAsync(isuser, props);

                if (context != null)
                {
                    if (context.IsNativeClient())
                    {
                        // The client is native, so this change in how to
                        // return the response is for better UX for the end user.
                        return this.LoadingPage(Input.ReturnUrl);
                    }

                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    return Redirect(Input.ReturnUrl);
                }

                if (Url.IsLocalUrl(Input.ReturnUrl))
                {
                    return Redirect(Input.ReturnUrl);
                }
                else if (string.IsNullOrEmpty(Input.ReturnUrl))
                {
                    return Redirect("~/");
                }
                else
                {
                    // user might have clicked on a malicious link - should be logged
                    throw new Exception("invalid return URL");
                }
            }
        }
        ModelState.AddModelError(string.Empty, LoginOptions.InvalidCredentialsErrorMessage);

        Input = new InputModel
        {
            ReturnUrl = Input.ReturnUrl,
            Email = Input.Email,
            FirstName = Input.FirstName,
            LastName = Input.LastName
        };
        return Page();
    }
}
