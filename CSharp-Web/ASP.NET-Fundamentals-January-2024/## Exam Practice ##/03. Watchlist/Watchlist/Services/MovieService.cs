namespace Watchlist.Services;

using System.Globalization;
using Data;
using Data.Models;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

public class MovieService : IMovieService
{
	private readonly WatchlistDbContext _dbContext;

	public MovieService(WatchlistDbContext dbContext)
	{
		this._dbContext = dbContext;
	}

	private Movie CreateNewMovie(MovieAddFormModel model)
	{
		return new Movie
		{
			Title = model.Title,
			Director = model.Director,
			ImageUrl = model.ImageUrl,
			Rating = model.Rating,
			GenreId = model.GenreId
		};
	}

	private async Task<bool> MovieExists(Movie movie)
	{
		return await this._dbContext
			.Movies
			.AnyAsync(m => m.Title == movie.Title &&
			               m.Director == movie.Director);
	}

	public async Task<IEnumerable<GenreViewModel>> GetGenresAsync()
	{
		IEnumerable<GenreViewModel> genres = await this._dbContext
			.Genres
			.Select(g => new GenreViewModel
			{
				Id = g.Id,
				Name = g.Name
			})
			.AsNoTracking()
			.ToArrayAsync();

		return genres;
	}

	public async Task<bool> AddAsync(MovieAddFormModel model)
	{
		var newMovie = this.CreateNewMovie(model);

		bool movieExists = await this.MovieExists(newMovie);

		if (movieExists)
		{
			return false;
		}

		await this._dbContext
			.Movies
			.AddAsync(newMovie);

		await this._dbContext.SaveChangesAsync();

		return true;
	}

	public async Task<IEnumerable<MovieViewModel>> AllAsync()
	{
		IEnumerable<MovieViewModel> movies = await this._dbContext
			.Movies
			.Select(m => new MovieViewModel
			{
				Id = m.Id,
				ImageUrl = m.ImageUrl,
				Title = m.Title,
				Director = m.Director,
				Rating = m.Rating.ToString(CultureInfo.InvariantCulture),
				Genre = m.Genre.Name
			})
			.AsNoTracking()
			.ToArrayAsync();

		return movies;
	}

	public async Task<bool> AddToCollectionAsync(string userId, int movieId)
	{
		bool alreadyAddedToCollection = await this._dbContext
			.UserMovies
			.AnyAsync(um => um.UserId == userId &&
			                um.MovieId == movieId);

		if (alreadyAddedToCollection)
		{
			return false;
		}

		var userMovie = new UserMovie
		{
			UserId = userId,
			MovieId = movieId
		};

		this._dbContext
			.UserMovies
			.Add(userMovie);

		await this._dbContext
			.SaveChangesAsync();

		return true;
	}

	public async Task<bool> RemoveFromCollectionAsync(string userId, int movieId)
	{
		var movieToRemove = await this._dbContext
			.UserMovies
			.FirstOrDefaultAsync(um => um.UserId == userId &&
			                           um.MovieId == movieId);

		if (movieToRemove == null)
		{
			return false;
		}

		this._dbContext
			.UserMovies
			.Remove(movieToRemove);

		await this._dbContext
			.SaveChangesAsync();

		return true;
	}

	public async Task<IEnumerable<MovieViewModel>> GetWatchedAsync(string userId)
	{
		IEnumerable<MovieViewModel> movies = await this._dbContext
			.UserMovies
			.Where(um => um.UserId == userId)
			.Select(m => new MovieViewModel
			{
				Id = m.MovieId,
				ImageUrl = m.Movie.ImageUrl,
				Title = m.Movie.Title,
				Director = m.Movie.Director,
				Rating = m.Movie.Rating.ToString(CultureInfo.InvariantCulture),
				Genre = m.Movie.Genre.Name
			})
			.AsNoTracking()
			.ToArrayAsync();

		return movies;
	}
}