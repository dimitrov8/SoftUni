﻿namespace TaskBoardApp.Areas.Controllers;

using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
	public IActionResult Index()
	{
		return this.View();
	}
}