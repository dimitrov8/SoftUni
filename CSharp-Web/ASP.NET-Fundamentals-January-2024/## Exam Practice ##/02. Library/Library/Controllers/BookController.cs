namespace Library.Controllers;

using Extensions;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

public class BookController : BaseController
{
	private readonly IBookService _bookService;

	public BookController(IBookService bookService)
	{
		this._bookService = bookService;
	}

	private async Task<IActionResult> HandleInvalidModelState(string viewName, BookAddFormModel model)
	{
		model.Categories = await this._bookService.GetCategoriesAsync();

		return this.View(viewName, model);
	}

	public async Task<IActionResult> All()
	{
		IEnumerable<BooksAllViewModel> model = await this._bookService
			.AllBooksAsync();

		return this.View(model);
	}

	public async Task<IActionResult> AddToCollection(int id)
	{
		try
		{
			string userId = this.User.GetId();

			bool addedSuccessful = await this._bookService.AddBookToCollectionAsync(userId, id);

			if (addedSuccessful == false)
			{
				this.TempData["ErrorMessage"] = "Book already owned!";
			}

			return this.RedirectToAction(nameof(this.All));
		}
		catch (Exception)
		{
			return this.BadRequest();
		}
	}

	public async Task<IActionResult> Add()
	{
		if (this.User.Identity.IsAuthenticated == false)
		{
			return this.Redirect("/Identity/Account/Login");
		}

		var viewModel = new BookAddFormModel
		{
			Categories = await this._bookService.GetCategoriesAsync()
		};

		return this.View(nameof(this.Add), viewModel);
	}

	[HttpPost]
	public async Task<IActionResult> Add(BookAddFormModel model)
	{
		model.Categories = await this._bookService.GetCategoriesAsync();

		if (!this.ModelState.IsValid)
		{
			return await this.HandleInvalidModelState(nameof(Add), model);
		}

		try
		{
			string userId = this.User.GetId();

			bool addedSuccessful = await this._bookService.AddAsync(userId, model);

			if (addedSuccessful == false)
			{
				this.TempData["ErrorMessage"] = "Book already added!";
			}

			return this.RedirectToAction(nameof(this.All), model);
		}
		catch (Exception)
		{
			this.ModelState.AddModelError(string.Empty, "An error occurred while adding the book.");

			return this.View(nameof(Add), model);
		}
	}
}