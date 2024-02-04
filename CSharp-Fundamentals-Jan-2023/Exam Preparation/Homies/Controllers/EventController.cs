namespace Homies.Controllers;

using Extensions;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Web.ViewModels;

public class EventController : Controller
{
	private readonly IEventService _eventService;

	public EventController(IEventService eventService)
	{
		this._eventService = eventService;
	}

	public async Task<IActionResult> Add()
	{
		if (!this.User.Identity.IsAuthenticated)
		{
			return this.Redirect("/Identity/Account/Login");
		}

		var viewModel = new EventFormModel
		{
			Types = await this._eventService.GetEventTypesAsync()
		};

		return this.View("Add", viewModel);
	}

	[HttpPost]
	public async Task<IActionResult> Add(EventFormModel model)
	{
		if (!this.ModelState.IsValid)
		{
			model.Types = await this._eventService.GetEventTypesAsync();

			return this.View("Add", model);
		}

		string organiserId = this.User.GetId();

		// TODO => CHECK IF DATES ARE SET PROPERLY

		if (DateTime.Parse(model.End) <= DateTime.Parse(model.Start))
		{
			this.ModelState.AddModelError(string.Empty, "End date must be after the start date.");
			this.ViewBag.CustomError = "End date must be after the start date.";
			model.Types = await this._eventService.GetEventTypesAsync();

			return this.View("Add", model);
		}

		try
		{
			await this._eventService.AddAsync(organiserId, model);

			return this.RedirectToAction("Index", "Home");
		}
		catch (Exception)
		{
			this.ModelState.AddModelError(string.Empty, "An error occurred while adding the event.");

			return this.View("Add", model);
		}
	}
}