// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

namespace TaskBoardApp.Areas.Identity.Pages.Account;

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

public class RegisterModel : PageModel
{
	private readonly SignInManager<IdentityUser> _signInManager;
	private readonly UserManager<IdentityUser> _userManager;
	private readonly IUserStore<IdentityUser> _userStore;
	private readonly IUserEmailStore<IdentityUser> _emailStore;
	private readonly ILogger<RegisterModel> _logger;
	private readonly IEmailSender _emailSender;

	public RegisterModel(
		UserManager<IdentityUser> userManager,
		IUserStore<IdentityUser> userStore,
		SignInManager<IdentityUser> signInManager,
		ILogger<RegisterModel> logger,
		IEmailSender emailSender)
	{
		this._userManager = userManager;
		this._userStore = userStore;
		this._emailStore = this.GetEmailStore();
		this._signInManager = signInManager;
		this._logger = logger;
		this._emailSender = emailSender;
	}

	/// <summary>
	///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
	///     directly from your code. This API may change or be removed in future releases.
	/// </summary>
	[BindProperty]
	public InputModel Input { get; set; }

	/// <summary>
	///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
	///     directly from your code. This API may change or be removed in future releases.
	/// </summary>
	public string ReturnUrl { get; set; }

	/// <summary>
	///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
	///     directly from your code. This API may change or be removed in future releases.
	/// </summary>
	public IList<AuthenticationScheme> ExternalLogins { get; set; }

	/// <summary>
	///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
	///     directly from your code. This API may change or be removed in future releases.
	/// </summary>
	public class InputModel
	{
		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}


	public async Task OnGetAsync(string returnUrl = null)
	{
		this.ReturnUrl = returnUrl;
		this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
	}

	public async Task<IActionResult> OnPostAsync(string returnUrl = null)
	{
		returnUrl ??= this.Url.Content("~/");
		this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

		if (this.ModelState.IsValid)
		{
			var user = this.CreateUser();

			await this._userStore.SetUserNameAsync(user, this.Input.Email, CancellationToken.None);
			await this._emailStore.SetEmailAsync(user, this.Input.Email, CancellationToken.None);
			var result = await this._userManager.CreateAsync(user, this.Input.Password);

			if (result.Succeeded)
			{
				this._logger.LogInformation("User created a new account with password.");

				string userId = await this._userManager.GetUserIdAsync(user);
				string code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
				code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
				string callbackUrl = this.Url.Page(
					"/Account/ConfirmEmail",
					null,
					new
					{
						area = "Identity",
						userId,
						code,
						returnUrl
					},
					this.Request.Scheme);

				await this._emailSender.SendEmailAsync(this.Input.Email, "Confirm your email",
					$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

				if (this._userManager.Options.SignIn.RequireConfirmedAccount)
				{
					return this.RedirectToPage("RegisterConfirmation", new
					{
						email = this.Input.Email,
						returnUrl
					});
				}

				await this._signInManager.SignInAsync(user, false);
				return this.LocalRedirect(returnUrl);
			}

			foreach (var error in result.Errors)
			{
				this.ModelState.AddModelError(string.Empty, error.Description);
			}
		}

		// If we got this far, something failed, redisplay form
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

	private IUserEmailStore<IdentityUser> GetEmailStore()
	{
		if (!this._userManager.SupportsUserEmail)
		{
			throw new NotSupportedException("The default UI requires a user store with email support.");
		}

		return (IUserEmailStore<IdentityUser>)this._userStore;
	}
}