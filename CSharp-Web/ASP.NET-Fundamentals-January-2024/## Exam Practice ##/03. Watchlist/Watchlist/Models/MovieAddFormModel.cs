namespace Watchlist.Models;

using System.ComponentModel.DataAnnotations;
using static Common.EntityValidations.Movie;

public class MovieAddFormModel
{
	[Required]
	[StringLength(MAX_TITLE_LENGTH)]
	[MinLength(MIN_TITLE_LENGTH)]
	public string Title { get; set; } = null!;

	[Required]
	[StringLength(MAX_DIRECTOR_LENGTH)]
	[MinLength(MIN_DIRECTOR_LENGTH)]
	public string Director { get; set; } = null!;

	[Required]
	[Url]
	public string ImageUrl { get; set; } = null!;

	[Required]
	[Range(MIN_RATING_VALUE, MAX_RATING_VALUE)]
	public decimal Rating { get; set; }

	public int GenreId { get; set; }

	public IEnumerable<GenreViewModel> Genres { get; set; } = null!;
}