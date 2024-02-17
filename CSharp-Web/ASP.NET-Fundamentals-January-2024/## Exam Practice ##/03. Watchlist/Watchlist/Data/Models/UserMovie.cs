namespace Watchlist.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserMovie
{
	[Required]
	public string UserId { get; set; } = null!;

	[ForeignKey(nameof(UserId))]
	public User User { get; set; } = null!;

	[Required]
	public int MovieId { get; set; }

	[ForeignKey(nameof(MovieId))]
	public Movie Movie { get; set; } = null!;
}