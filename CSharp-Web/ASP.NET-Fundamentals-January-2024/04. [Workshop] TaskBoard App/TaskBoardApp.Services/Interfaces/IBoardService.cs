namespace TaskBoardApp.Services.Interfaces;

using Web.ViewModels.Board;

public interface IBoardService
{
	Task<IEnumerable<BoardViewModel>> AllAsync();

	Task<IEnumerable<BoardSelectViewModel>> AllForSelectAsync();

	Task<bool> ExistsByIdAsync(int id);
}