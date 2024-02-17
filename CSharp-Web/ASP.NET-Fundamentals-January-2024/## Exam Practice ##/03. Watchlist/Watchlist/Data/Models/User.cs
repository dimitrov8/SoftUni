namespace Watchlist.Data.Models;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
	public ICollection<UserMovie> UsersMovies { get; set; } = new HashSet<UserMovie>();
}