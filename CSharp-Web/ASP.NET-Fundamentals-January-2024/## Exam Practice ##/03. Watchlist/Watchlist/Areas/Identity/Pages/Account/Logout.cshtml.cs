#nullable disable

namespace Watchlist.Areas.Identity.Pages.Account;

using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LogoutModel : PageModel
{
	private readonly SignInManager<User> _signInManager;

	public LogoutModel(SignInManager<User> signInManager)
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

		return this.RedirectToPage();
	}
}