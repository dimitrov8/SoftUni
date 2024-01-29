namespace TaskBoardApp.Web.ViewModels.Task;

public class TaskViewModel
{
	public string Id { get; init; } = null!;

	public string Title { get; init; } = null!;

	public string Description { get; init; } = null!;

	public string Owner { get; set; } = null!;
}