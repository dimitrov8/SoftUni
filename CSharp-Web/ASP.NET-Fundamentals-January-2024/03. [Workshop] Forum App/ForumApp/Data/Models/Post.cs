namespace ForumApp.Data.Models;

using System.ComponentModel.DataAnnotations;
using static DataConstants;

public class Post
{
	public int Id { get; init; }

	[Required]
	[MaxLength(POST_TITLE_MAX_LENGTH)]
	public string Title { get; set; } = null!;

	[Required]
	[MaxLength(POST_CONTENT_MAX_LENGTH)]
	public string Content { get; set; } = null!;
}