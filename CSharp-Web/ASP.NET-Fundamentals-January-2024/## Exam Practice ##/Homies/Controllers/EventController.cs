namespace Homies.Controllers;

using Extensions;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Web.ViewModels;

public class EventController : BaseController
{
	private readonly IEventService _eventService;

	public EventController(IEventService eventService)
	{
		this._eventService = eventService;
	}

	private async Task<IActionResult> HandleInvalidModelState(string viewName, EventFormModel model)
	{
		model.Types = await this._eventService.GetTypesAsync();

		return this.View(viewName, model);
	}

	private async Task<IActionResult> HandleInvalidDate(EventFormModel model)
	{
		this.ModelState.AddModelError(string.Empty, "End date must be after the start date.");
		this.TempData["CustomError"] = "End date must be after the start date.";

		return await this.HandleInvalidModelState("Add", model);
	}

	public async Task<IActionResult> Add()
	{
		if (!this.User.Identity.IsAuthenticated)
		{
			return this.Redirect("/Identity/Account/Login");
		}

		var viewModel = new EventFormModel
		{
			Types = await this._eventService.GetTypesAsync()
		};

		return this.View("Add", viewModel);
	}

	[HttpPost]
	public async Task<IActionResult> Add(EventFormModel model)
	{
		model.Types = await this._eventService.GetTypesAsync();

		if (!this.ModelState.IsValid)
		{
			return await this.HandleInvalidModelState("Add", model);
		}

		string organiserId = this.User.GetId();

		if (DateTime.Parse(model.End) <= DateTime.Parse(model.Start))
		{
			return await this.HandleInvalidDate(model);
		}

		try
		{
			await this._eventService.AddAsync(organiserId, model);

			return this.RedirectToAction("All", "Event");
		}
		catch (Exception)
		{
			this.ModelState.AddModelError(string.Empty, "An error occurred while adding the event.");

			return this.View("Add", model);
		}
	}

	public async Task<IActionResult> All()
	{
		IEnumerable<EventAllViewModel> allBoards = await this._eventService.GetAllAsync();

		return this.View(allBoards);
	}

	public async Task<IActionResult> Details(int eventId)
	{
		try
		{
			var viewModel = await this._eventService.ViewDetailsAsync(eventId);

			if (viewModel == null)
			{
				return this.NotFound();
			}

			return this.View(viewModel);
		}
		catch (Exception)
		{
			return this.StatusCode(500, "An error occurred while processing your request.");
		}
	}

	public async Task<IActionResult> Edit(int eventId)
	{
		try
		{
			var viewModel = await this._eventService.GetForEditAsync(eventId);

			if (viewModel == null)
			{
				return this.NotFound();
			}

			if (viewModel.OrganaiserId != this.User.GetId())
			{
				return this.Forbid();
			}

			return this.View(viewModel);
		}
		catch (Exception)
		{
			return this.RedirectToAction("All", "Event");
		}
	}

	[HttpPost]
	public async Task<IActionResult> Edit(int eventId, EventFormModel model)
	{
		model.Types = await this._eventService.GetTypesAsync();

		if (!this.ModelState.IsValid)
		{
			return this.View("Edit", model);
		}

		if (DateTime.Parse(model.End) <= DateTime.Parse(model.Start))
		{
			return await this.HandleInvalidDate(model);
		}

		try
		{
			await this._eventService.EditAsync(eventId, model);

			return this.RedirectToAction("All", "Event");
		}
		catch (Exception)
		{
			return this.NotFound();
		}
	}

	public async Task<ActionResult<IEnumerable<EventJoinedViewModel>>> Joined()
	{
		string userId = this.User.GetId();

		try
		{
			IEnumerable<EventJoinedViewModel> events = await this._eventService.JoinedAsync(userId);

			return this.View(events);
		}
		catch (Exception)
		{
			return this.NotFound();
		}
	}

	public async Task<IActionResult> Join(int eventId)
	{
		try
		{
			string userId = this.User.GetId();
			bool joinSuccessful = await this._eventService.JoinAsync(eventId, userId);

			if (!joinSuccessful)
			{
				this.TempData["ErrorMessage"] = "You have already joined this event.";

				return this.RedirectToAction("All", "Event");
			}

			return this.RedirectToAction("Joined", "Event");
		}
		catch (Exception)
		{
			return this.NotFound();
		}
	}

	public async Task<IActionResult> Leave(int eventId)
	{
		try
		{
			string userId = this.User.GetId();
			bool leaveSuccessful = await this._eventService.LeaveAsync(eventId, userId);

			if (!leaveSuccessful)
			{
				this.TempData["ErrorMessage"] = "You are not participating in this event.";
			}

			return this.RedirectToAction("All", "Event");
		}
		catch (Exception)
		{
			return this.NotFound();
		}
	}
}