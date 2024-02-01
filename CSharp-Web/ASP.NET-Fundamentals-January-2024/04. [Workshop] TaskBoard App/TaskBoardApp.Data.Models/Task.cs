namespace TaskBoardApp.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static Common.EntityValidationConstants.Task;

public class Task
{
	public Task()
	{
		this.Id = Guid.NewGuid();
	}

	[Key]
	public Guid Id { get; init; }

	[Required]
	[MaxLength(MAX_TITLE_LENGTH)]
	public string Title { get; set; } = null!;

	[Required]
	[MaxLength(MAX_DESCRIPTION_LENGTH)]
	public string Description { get; set; } = null!;

	public DateTime CreatedOn { get; set; }

	[ForeignKey(nameof(Board))]
	public int BoardId { get; set; }

	public Board Board { get; set; }

	[Required]
	[ForeignKey(nameof(Owner))]
	public string OwnerId { get; set; } = null!;

	public IdentityUser Owner { get; set; } = null!;
}