﻿#nullable disable

namespace TaskBoardApp.Areas.Identity.Pages.Account;

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
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between {2} and {1} characters long!")]
		[Display(Name = "Username")]
		public string Username { get; set; }

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
			var result = await this._signInManager.PasswordSignInAsync(this.Input.Username, this.Input.Password, true, false);

			if (result.Succeeded)
			{
				return this.LocalRedirect(returnUrl);
			}

			this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
			return this.Page();
		}

		return this.Page();
	}
}