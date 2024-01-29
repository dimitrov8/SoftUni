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
			model.Boards = await this._boardService.AllForSelectAsync();

			return this.View(model);
		}

		bool boardExists = await this._boardService.ExistsByIdAsync(model.BoardId);

		if (!boardExists)
		{
			this.ModelState.AddModelError(nameof(model.BoardId), "Selected board does not exists!");
			model.Boards = await this._boardService.AllForSelectAsync();

			return this.View(model);
		}

		string currentUserId = this.User.GetId();
		await this._taskService.AddAsync(currentUserId, model);

		return this.RedirectToAction("All", "Board");
	}
}