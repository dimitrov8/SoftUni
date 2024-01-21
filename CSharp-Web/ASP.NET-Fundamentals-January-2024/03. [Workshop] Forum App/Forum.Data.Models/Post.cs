namespace Forum.Data.Models;

using System.ComponentModel.DataAnnotations;
using static Common.Validations.EntityValidations.Post;

public class Post
{
	public Post()
	{
		this.Id = Guid.NewGuid();
	}

	[Key]
	public Guid Id { get; set; }

	[Required]
	[MaxLength(POST_TITLE_MAX_LENGTH)]
	public string Title { get; set; } = null!;

	[Required]
	[MaxLength(POST_CONTENT_MAX_LENGTH)]
	public string Content { get; set; } = null!;
}