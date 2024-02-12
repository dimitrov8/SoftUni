namespace Library.Models;

using System.ComponentModel.DataAnnotations;
using static Common.EntityValidations.Book;
using static Common.EntityValidations.Category;

public class BookAddFormModel
{
	[Required]
	[StringLength(MAX_TITLE_LENGTH)]
	[MinLength(MIN_TITLE_LENGTH)]
	public string Title { get; set; } = null!;

	[Required]
	[StringLength(MAX_AUTHOR_LENGTH)]
	[MinLength(MIN_AUTHOR_LENGTH)]
	public string Author { get; set; } = null!;

	[Required]
	[StringLength(MAX_DESCRIPTION_LENGTH)]
	[MinLength(MIN_DESCRIPTION_LENGTH)]
	public string Description { get; set; } = null!;

	[Required]
	[Url(ErrorMessage = "Invalid URL format")]
	public string Url { get; set; } = null!;

	[Required]
	[StringLength(MAX_RATING_VALUE)]
	[MinLength(MIN_RATING_VALUE)]
	public string Rating { get; set; } = null!;

	[Required]
	[Range(MIN_CATEGORY_VALUE, MAX_CATEGORY_VALUE)]
	public int CategoryId { get; set; }

	public IEnumerable<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();
}