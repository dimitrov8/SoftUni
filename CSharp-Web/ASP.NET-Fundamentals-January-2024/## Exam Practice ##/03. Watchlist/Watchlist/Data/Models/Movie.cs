namespace Watchlist.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Common.EntityValidations.Movie;

public class Movie
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(MAX_TITLE_LENGTH)]
	public string Title { get; set; } = null!;

	[Required]
	[MaxLength(MAX_DIRECTOR_LENGTH)]
	public string Director { get; set; } = null!;

	[Required]
	public string ImageUrl { get; set; } = null!;

	[Required]
	[Range(MIN_RATING_VALUE, MAX_RATING_VALUE)]
	public decimal Rating { get; set; }

	[Required]
	public int GenreId { get; set; }

	[Required]
	[ForeignKey(nameof(GenreId))]
	public Genre Genre { get; set; } = null!;

	public ICollection<UserMovie> UserMovies { get; set; } = new HashSet<UserMovie>();
}