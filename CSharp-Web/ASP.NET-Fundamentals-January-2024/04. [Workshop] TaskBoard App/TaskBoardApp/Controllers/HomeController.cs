namespace TaskBoardApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Web.ViewModels.Home;

public class HomeController : Controller
{
	private readonly ITaskService _taskService;
	private readonly IBoardService _boardService;

	public HomeController(ITaskService taskService, IBoardService boardService)
	{
		this._taskService = taskService;
		this._boardService = boardService;
	}

	public async Task<IActionResult> Index()
	{
		IEnumerable<HomeBoardModel> boardsWithTasksCount = await this._boardService.GetBoardsWithTasksCountAsync();
		int userTaskCount = await this._taskService.GetUserTasksCountAsync(this.User);

		var homeModel = new HomeViewModel
		{
			AllTasksCount = await this._taskService.GetAllTasksCountAsync(),
			BoardsWithTasksCount = boardsWithTasksCount,
			UserTasksCount = userTaskCount
		};

		return this.View(homeModel);
	}
}