namespace Homies.Controllers;

using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

public class HomeController : BaseController
{
	[AllowAnonymous]
	public IActionResult Index()
	{
		if (this.User.Identity.IsAuthenticated)
		{
			return this.RedirectToAction("All", "Event");
		}

		return this.View("Index");
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
	}
}