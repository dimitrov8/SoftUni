namespace Watchlist.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

public class WatchlistDbContext : IdentityDbContext<User>
{
	public WatchlistDbContext(DbContextOptions<WatchlistDbContext> options)
		: base(options)
	{
	}

	public DbSet<Movie> Movies { get; set; } = null!;
	public DbSet<Genre> Genres { get; set; } = null!;

	public DbSet<UserMovie> UserMovies { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<UserMovie>()
			.HasKey(um => new { um.UserId, um.MovieId });

		builder.Entity<Movie>()
			.Property(m => m.Rating)
			.HasPrecision(4, 2);

		builder.Entity<UserMovie>()
			.HasOne(um => um.User)
			.WithMany(u => u.UsersMovies)
			.HasForeignKey(um => um.UserId);

		builder.Entity<UserMovie>()
			.HasOne(um => um.Movie)
			.WithMany(u => u.UserMovies)
			.HasForeignKey(um => um.MovieId);

		builder.Entity<User>()
			.Property(u => u.UserName)
			.HasMaxLength(20)
			.IsRequired();

		builder.Entity<User>()
			.Property(u => u.Email)
			.HasMaxLength(60)
			.IsRequired();

		builder
			.Entity<Genre>()
			.HasData(new Genre
				{
					Id = 1,
					Name = "Action"
				},
				new Genre
				{
					Id = 2,
					Name = "Comedy"
				},
				new Genre
				{
					Id = 3,
					Name = "Drama"
				},
				new Genre
				{
					Id = 4,
					Name = "Horror"
				},
				new Genre
				{
					Id = 5,
					Name = "Romantic"
				});

		base.OnModelCreating(builder);
	}
}