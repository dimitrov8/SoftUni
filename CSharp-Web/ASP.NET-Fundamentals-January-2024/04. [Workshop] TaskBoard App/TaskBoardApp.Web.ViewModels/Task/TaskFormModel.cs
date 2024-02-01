namespace TaskBoardApp.Web.ViewModels.Task;

using System.ComponentModel.DataAnnotations;
using Board;
using static Common.EntityValidationConstants.Task;

public class TaskFormModel
{
	[Required]
	[StringLength(MAX_TITLE_LENGTH, MinimumLength = MIN_TITLE_LENGTH,
		ErrorMessage = "Title should be at least {2} characters long!")]
	public string Title { get; set; } = null!;

	[Required]
	[StringLength(MAX_DESCRIPTION_LENGTH, MinimumLength = MIN_DESCRIPTION_LENGTH,
		ErrorMessage = "Description should be at least {2} characters long.")]
	public string Description { get; set; } = null!;

	[Display(Name = "Board")]
	public int BoardId { get; set; }

	public IEnumerable<BoardSelectViewModel> Boards { get; set; } = null!;
}