namespace Watchlist.Controllers;

using Extensions;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

public class MovieController : BaseController
{
	private readonly IMovieService _movieService;

	public MovieController(IMovieService movieService)
	{
		this._movieService = movieService;
	}

	private async Task HandleInvalidModelState(string viewName, MovieAddFormModel model)
	{
		model.Genres = await this._movieService.GetGenresAsync();

		this.View(viewName);
	}

	public async Task<IActionResult> Add()
	{
		var viewModel = new MovieAddFormModel
		{
			Genres = await this._movieService.GetGenresAsync()
		};

		return this.View(viewModel);
	}

	[HttpPost]
	public async Task<IActionResult> Add(MovieAddFormModel model)
	{
		model.Genres = await this._movieService.GetGenresAsync();

		if (!this.ModelState.IsValid)
		{
			await this.HandleInvalidModelState(nameof(Add), model);
		}

		try
		{
			bool addedSuccessfully = await this._movieService.AddAsync(model);

			if (addedSuccessfully == false)
			{
				this.TempData["ErrorMessage"] = "Movie already added!";
			}

			return this.RedirectToAction(nameof(this.All));
		}
		catch (Exception)
		{
			this.ModelState.AddModelError(string.Empty, "An error occurred while adding the movie.");
			model.Genres = await this._movieService.GetGenresAsync();

			return this.View(model);
		}
	}

	public async Task<IActionResult> All()
	{
		IEnumerable<MovieViewModel> viewModel = await this._movieService.AllAsync();

		return this.View(viewModel);
	}

	public async Task<IActionResult> AddToCollection(int movieId)
	{
		try
		{
			string userId = this.User.GetId();

			bool addedSuccessfully = await this._movieService.AddToCollectionAsync(userId, movieId);

			if (addedSuccessfully == false)
			{
				this.TempData["ErrorMessage"] = "Movie already added to your collection!";
			}

			return this.RedirectToAction(nameof(this.All));
		}
		catch (Exception)
		{
			return this.BadRequest();
		}
	}

	public async Task<IActionResult> RemoveFromCollection(int movieId)
	{
		string userId = this.User.GetId();

		if (movieId <= 0)
		{
			return this.BadRequest("Invalid movie ID");
		}

		bool movieOwned = await this._movieService
			.RemoveFromCollectionAsync(userId, movieId);

		if (movieOwned == false)
		{
			return this.NotFound();
		}

		return this.RedirectToAction(nameof(this.Watched));
	}

	public async Task<IActionResult> Watched()
	{
		string userId = this.User.GetId();
		IEnumerable<MovieViewModel> viewModel = await this._movieService.GetWatchedAsync(userId);
		return this.View(viewModel);
	}
}