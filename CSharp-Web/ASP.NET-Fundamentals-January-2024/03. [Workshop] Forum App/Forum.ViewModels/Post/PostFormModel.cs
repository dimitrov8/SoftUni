namespace Forum.ViewModels.Post;

using System.ComponentModel.DataAnnotations;
using static Common.Validations.EntityValidations.Post;

public class PostFormModel
{
	[Required]
	[StringLength(POST_TITLE_MAX_LENGTH, MinimumLength = POST_TITLE_MIN_LENGTH)]
	public string Title { get; set; } = null!;

	[Required]
	[StringLength(POST_CONTENT_MAX_LENGTH, MinimumLength = POST_CONTENT_MIN_LENGTH)]
	public string Content { get; set; } = null!;
}