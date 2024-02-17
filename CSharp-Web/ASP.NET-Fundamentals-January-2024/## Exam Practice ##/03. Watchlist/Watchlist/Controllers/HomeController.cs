namespace Watchlist.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class HomeController : BaseController
{
	[AllowAnonymous]
	public IActionResult Index()
	{
		if (this.User.Identity?.IsAuthenticated ?? false)
		{
			return this.RedirectToAction("All", "Movie");
		}

		return this.View();
	}
}