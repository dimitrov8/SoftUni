namespace Watchlist.Data.Models;

using System.ComponentModel.DataAnnotations;
using static Common.EntityValidations.Genre;

public class Genre
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(MAX_NAME_LENGTH)]
	public string Name { get; set; } = null!;

	public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
}