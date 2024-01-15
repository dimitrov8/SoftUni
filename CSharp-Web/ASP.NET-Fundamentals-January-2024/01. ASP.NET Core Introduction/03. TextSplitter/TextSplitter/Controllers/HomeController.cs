namespace TextSplitter.Controllers;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Models;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger)
	{
		this._logger = logger;
	}

	public IActionResult Index(TextViewModel model)
	{
		return this.View(model);
	}

	[HttpPost]
	public IActionResult Split(TextViewModel model)
	{
		string[] splitTextArray = model
			.Text
			.Split(" ", StringSplitOptions.RemoveEmptyEntries)
			.ToArray();

		model.SplitText = string.Join(Environment.NewLine, splitTextArray);

		return this.RedirectToAction("Index", model);
	}

	public IActionResult Privacy()
	{
		return this.View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
	}
}