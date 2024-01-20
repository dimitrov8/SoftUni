namespace ForumApp.Models.Post;

using System.ComponentModel.DataAnnotations;
using static Data.DataConstants;

public class PostFormModel
{
	[Required]
	[StringLength(POST_TITLE_MAX_LENGTH, MinimumLength = POST_CONTENT_MIN_LENGTH)]
	public string Title { get; set; } = null!;

	[StringLength(POST_CONTENT_MAX_LENGTH, MinimumLength = POST_CONTENT_MIN_LENGTH)]
	public string Content { get; set; } = null!;
}