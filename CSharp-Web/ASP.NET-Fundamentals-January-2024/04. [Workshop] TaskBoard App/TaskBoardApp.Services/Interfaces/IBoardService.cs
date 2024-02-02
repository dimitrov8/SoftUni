namespace TaskBoardApp.Services.Interfaces;

using Web.ViewModels.Board;
using Web.ViewModels.Home;

public interface IBoardService
{
	Task<IEnumerable<BoardViewModel>> AllAsync();

	Task<IEnumerable<BoardSelectViewModel>> AllForSelectAsync();

	Task<bool> ExistsByIdAsync(int id);

	Task<IEnumerable<HomeBoardModel>> GetBoardsWithTasksCountAsync();
}