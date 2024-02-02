namespace TaskBoardApp.Controllers;

using Extensions;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Web.ViewModels.Task;

public class TaskController : Controller
{
	private readonly IBoardService _boardService;
	private readonly ITaskService _taskService;

	public TaskController(IBoardService boardService, ITaskService taskService)
	{
		this._boardService = boardService;
		this._taskService = taskService;
	}

	public async Task<IActionResult> Create()
	{
		if (!this.User.Identity.IsAuthenticated)
		{
			return this.Redirect("/Identity/Account/Login");
		}


		var viewModel = new TaskFormModel
		{
			Boards = await this._boardService.AllForSelectAsync()
		};

		return this.View(viewModel);
	}

	[HttpPost]
	public async Task<IActionResult> Create(TaskFormModel model)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(model);
		}

		bool boardExists = await this._boardService.ExistsByIdAsync(model.BoardId);

		if (!boardExists)
		{
			this.ModelState.AddModelError(nameof(model.BoardId), "Selected board does not exists!");

			return this.View(model);
		}

		string currentUserId = this.User.GetId();
		await this._taskService.AddAsync(currentUserId, model);

		return this.RedirectToAction("All", "Board");
	}

	public async Task<IActionResult> Details(string id)
	{
		try
		{
			var viewModel = await this._taskService.GetDetailsByIdAsync(id);

			return this.View(viewModel);
		}
		catch (Exception)
		{
			return this.RedirectToAction("All", "Board");
		}
	}

	public async Task<IActionResult> Edit(string id)
	{
		try
		{
			var viewModel = await this._taskService.GetForEditAsync(id);

			return this.View(viewModel);
		}
		catch (Exception)
		{
			return this.RedirectToAction("All", "Board");
		}
	}

	[HttpPost]
	public async Task<IActionResult> Edit(string id, TaskFormModel model)
	{
		try
		{
			await this._taskService.EditAsync(id, model);

			return this.RedirectToAction("All", "Board");
		}
		catch (Exception)
		{
			return this.NotFound();
		}
	}

	public async Task<IActionResult> Delete(string id)
	{
		try
		{
			var viewModel = await this._taskService.GetForDeleteAsync(id);

			return this.View(viewModel);
		}
		catch (Exception)
		{
			return this.NotFound();
		}
	}

	[HttpPost]
	public async Task<IActionResult> Delete(TaskViewModel model)
	{
		try
		{
			await this._taskService.DeleteAsync(model);

			return this.RedirectToAction("All", "Board");
		}
		catch (Exception)
		{
			return this.NotFound();
		}
	}
}