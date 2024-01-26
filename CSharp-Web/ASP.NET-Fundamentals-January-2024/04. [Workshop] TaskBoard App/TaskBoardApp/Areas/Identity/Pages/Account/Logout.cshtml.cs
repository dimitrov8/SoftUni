#nullable disable

namespace TaskBoardApp.Areas.Identity.Pages.Account;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LogoutModel : PageModel
{
	private readonly SignInManager<IdentityUser> _signInManager;

	public LogoutModel(SignInManager<IdentityUser> signInManager)
	{
		this._signInManager = signInManager;
	}

	public async Task<IActionResult> OnPost(string returnUrl = null)
	{
		await this._signInManager.SignOutAsync();

		if (returnUrl != null)
		{
			return this.LocalRedirect(returnUrl);
		}

		// This needs to be a redirect so that the browser performs a new
		// request and the identity for the user gets updated.
		return this.RedirectToPage();
	}
}