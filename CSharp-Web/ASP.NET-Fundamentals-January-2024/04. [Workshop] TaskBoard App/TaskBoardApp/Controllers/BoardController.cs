namespace TaskBoardApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Web.ViewModels.Board;

public class BoardController : Controller
{
	private readonly IBoardService _boardService;

	public BoardController(IBoardService boardService)
	{
		this._boardService = boardService;
	}

	public async Task<IActionResult> All()
	{
		IEnumerable<BoardViewModel> allBoards =
			await this._boardService.AllAsync();

		return this.View(allBoards);
	}
}