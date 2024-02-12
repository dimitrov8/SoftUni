namespace Library.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Common.EntityValidations.Book;

public class Book
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(MAX_TITLE_LENGTH)]
	public string Title { get; set; } = null!;

	[Required]
	[MaxLength(MAX_AUTHOR_LENGTH)]
	public string Author { get; set; } = null!;

	[Required]
	[MaxLength(MAX_DESCRIPTION_LENGTH)]
	public string Description { get; set; } = null!;

	public string ImageUrl { get; set; } = null!;

	[Required]
	[MaxLength(MAX_RATING_VALUE)]
	public decimal Rating { get; set; }

	[Required]
	public int CategoryId { get; set; }

	[Required]
	[ForeignKey(nameof(CategoryId))]
	public Category Category { get; set; } = null!;

	public ICollection<IdentityUserBook> UsersBooks = new HashSet<IdentityUserBook>();
}