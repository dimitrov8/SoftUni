namespace MVCIntroDemo.Controllers;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Models;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> logger;

	public HomeController(ILogger<HomeController> logger)
	{
		this.logger = logger;
	}

	public IActionResult Index()
	{
		this.ViewBag.Message = "Hello World!";
		return this.View();
	}

	public IActionResult Privacy()
	{
		return this.View();
	}

	public IActionResult About()
	{
		this.ViewBag.Message = "This is an ASP.NET Core MVC app.";
		return this.View();
	}

	public IActionResult Numbers()
	{
		return this.View();
	}

	public IActionResult NumbersToN(int count = 3)
	{
		this.ViewBag.Count = count;
		return this.View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
	}
}