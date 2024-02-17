namespace Watchlist.Services.Interfaces;

using Models;

public interface IMovieService
{
	Task<IEnumerable<GenreViewModel>> GetGenresAsync();

	Task<bool> AddAsync(MovieAddFormModel model);

	Task<IEnumerable<MovieViewModel>> AllAsync();

	Task<bool> AddToCollectionAsync(string userId, int movieId);

	Task<bool> RemoveFromCollectionAsync(string userId, int movieId);

	Task<IEnumerable<MovieViewModel>> GetWatchedAsync(string userId);
}