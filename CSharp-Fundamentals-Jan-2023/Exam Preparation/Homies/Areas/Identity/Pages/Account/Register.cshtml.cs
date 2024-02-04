#nullable disable

namespace Homies.Areas.Identity.Pages.Account;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class RegisterModel : PageModel
{
	private readonly SignInManager<IdentityUser> _signInManager;
	private readonly UserManager<IdentityUser> _userManager;
	private readonly IUserStore<IdentityUser> _userStore;

	public RegisterModel(
		UserManager<IdentityUser> userManager,
		IUserStore<IdentityUser> userStore,
		SignInManager<IdentityUser> signInManager)
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


	public async Task OnGetAsync(string returnUrl = null)
	{
		this.ReturnUrl = returnUrl;
	}

	public async Task<IActionResult> OnPostAsync(string returnUrl = null)
	{
		returnUrl ??= this.Url.Content("~/");

		if (this.ModelState.IsValid)
		{
			var user = this.CreateUser();

			await this._userStore.SetUserNameAsync(user, this.Input.Email, CancellationToken.None);
			var result = await this._userManager.CreateAsync(user, this.Input.Password);

			if (result.Succeeded)
			{
				await this._signInManager.SignInAsync(user, false);
				return this.LocalRedirect(returnUrl);
			}

			foreach (var error in result.Errors)
			{
				this.ModelState.AddModelError(string.Empty, error.Description);
			}
		}

		return this.Page();
	}

	private IdentityUser CreateUser()
	{
		try
		{
			return Activator.CreateInstance<IdentityUser>();
		}
		catch
		{
			throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
			                                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
			                                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
		}
	}
}