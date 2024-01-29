namespace TaskBoardApp.Web.ViewModels.Board;

using Task;

public class BoardViewModel
{
	public int Id { get; init; }

	public string Name { get; init; } = null!;

	public ICollection<TaskViewModel> Tasks { get; set; } = null!;
}