namespace TaskBoardApp.Web.ViewModels.Home;

public class HomeViewModel
{
	public int AllTasksCount { get; set; }

	public IEnumerable<HomeBoardModel> BoardsWithTasksCount { get; set; } = new List<HomeBoardModel>();

	public int UserTasksCount { get; set; }
}