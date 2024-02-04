#nullable disable

namespace Homies.Areas.Identity.Pages.Account;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LoginModel : PageModel
{
	private readonly SignInManager<IdentityUser> _signInManager;

	public LoginModel(SignInManager<IdentityUser> signInManager)
	{
		this._signInManager = signInManager;
	}

	[BindProperty]
	public InputModel Input { get; set; }

	public string ReturnUrl { get; set; }

	[TempData]
	public string ErrorMessage { get; set; }

	public class InputModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}

	public async Task OnGetAsync(string returnUrl = null)
	{
		if (!string.IsNullOrEmpty(this.ErrorMessage))
		{
			this.ModelState.AddModelError(string.Empty, this.ErrorMessage);
		}

		returnUrl ??= this.Url.Content("~/");

		// Clear the existing external cookie to ensure a clean login process
		await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

		this.ReturnUrl = returnUrl;
	}

	public async Task<IActionResult> OnPostAsync(string returnUrl = null)
	{
		returnUrl ??= this.Url.Content("~/");

		if (this.ModelState.IsValid)
		{
			// This doesn't count login failures towards account lockout
			// To enable password failures to trigger account lockout, set lockoutOnFailure: true
			var result = await this._signInManager.PasswordSignInAsync(this.Input.Email, this.Input.Password, true, false);

			if (result.Succeeded)
			{
				return this.LocalRedirect(returnUrl);
			}

			this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
			return this.Page();
		}

		// If we got this far, something failed, redisplay form
		return this.Page();
	}
}