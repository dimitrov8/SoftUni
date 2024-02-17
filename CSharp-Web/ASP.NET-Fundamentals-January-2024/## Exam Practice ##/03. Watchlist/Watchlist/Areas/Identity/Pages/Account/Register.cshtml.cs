#nullable disable

namespace Watchlist.Areas.Identity.Pages.Account;

using System.ComponentModel.DataAnnotations;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class RegisterModel : PageModel
{
	private readonly SignInManager<User> _signInManager;
	private readonly UserManager<User> _userManager;
	private readonly IUserStore<User> _userStore;

	public RegisterModel(
		UserManager<User> userManager,
		IUserStore<User> userStore,
		SignInManager<User> signInManager)
	{
		this._userManager = userManager;
		this._userStore = userStore;
		this._signInManager = signInManager;
	}

	[BindProperty]
	public InputModel Input { get; set; }

	public string ReturnUrl { get; set; }

	public class InputModel
	{
		[Required]
		[Display(Name = "Username")]
		public string UserName { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}

	public async Task<IActionResult> OnGetAsync(string returnUrl = null)
	{
		this.ReturnUrl = returnUrl;

		if (this.User.Identity?.IsAuthenticated ?? false)
		{
			return this.RedirectToAction("All", "Movie");
		}

		return this.Page();
	}

	public async Task<IActionResult> OnPostAsync(string returnUrl = null)
	{
		returnUrl ??= this.Url.Content("~/");

		if (this.ModelState.IsValid)
		{
			var user = this.CreateUser();

			user.Email = this.Input.Email;

			await this._userStore.SetUserNameAsync(user, this.Input.UserName, CancellationToken.None);
			var result = await this._userManager.CreateAsync(user, this.Input.Password);

			if (result.Succeeded)
			{
				return this.Redirect("/Identity/Account/Login");
			}

			foreach (var error in result.Errors)
			{
				this.ModelState.AddModelError(string.Empty, error.Description);
			}
		}

		return this.Page();
	}

	private User CreateUser()
	{
		return new User();
	}
}