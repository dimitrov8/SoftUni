namespace TaskBoardApp.Data.Models;

using System.ComponentModel.DataAnnotations;
using static Common.EntityValidationConstants.Board;

public class Board
{
	public Board()
	{
		this.Tasks = new HashSet<Task>();
	}

	[Key]
	public int Id { get; init; }

	[Required]
	[MaxLength(MAX_BOARD_NAME)]
	public string Name { get; init; } = null!;

	public ICollection<Task> Tasks { get; set; }
}