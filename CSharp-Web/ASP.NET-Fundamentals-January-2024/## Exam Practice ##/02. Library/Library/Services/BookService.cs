namespace Library.Services;

using Data;
using Data.Models;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

public class BookService : IBookService
{
	private readonly LibraryDbContext _dbContext;

	public BookService(LibraryDbContext dbContext)
	{
		this._dbContext = dbContext;
	}

	private Book CreateNewBook(string userId, BookAddFormModel model)
	{
		return new Book
		{
			Title = model.Title,
			Author = model.Author,
			Description = model.Description,
			ImageUrl = model.Url,
			Rating = decimal.Parse(model.Rating),
			CategoryId = model.CategoryId
		};
	}

	private async Task<bool> BookExistsAsync(Book newBook)
	{
		return await this._dbContext
			.Books
			.AnyAsync(b =>
				b.Title == newBook.Title &&
				b.Author == newBook.Author &&
				b.Description == newBook.Description &&
				b.ImageUrl == newBook.ImageUrl &&
				b.Rating == newBook.Rating &&
				b.CategoryId == newBook.CategoryId);
	}

	public async Task<IEnumerable<BooksAllViewModel>> AllBooksAsync()
	{
		IEnumerable<BooksAllViewModel> books = await this._dbContext
			.Books
			.Select(b => new BooksAllViewModel
			{
				Id = b.Id,
				Title = b.Title,
				Author = b.Author,
				Rating = b.Rating,
				ImageUrl = b.ImageUrl,
				Category = b.Category.Name
			})
			.AsNoTracking()
			.ToArrayAsync();

		return books;
	}

	public async Task<bool> AddBookToCollectionAsync(string userId, int id)
	{
		bool alreadyOwned = await this._dbContext
			.IdentityUserBooks
			.AnyAsync(iup => iup.CollectorId == userId && iup.BookId == id);

		if (alreadyOwned)
		{
			return false;
		}

		var userBook = new IdentityUserBook
		{
			CollectorId = userId,
			BookId = id
		};

		await this._dbContext
			.IdentityUserBooks
			.AddAsync(userBook);

		await this._dbContext
			.SaveChangesAsync();

		return true;
	}

	public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
	{
		IEnumerable<CategoryViewModel> allCategories = await this._dbContext
			.Categories
			.Select(c => new CategoryViewModel
			{
				Id = c.Id,
				Name = c.Name
			})
			.AsNoTracking()
			.ToArrayAsync();

		return allCategories;
	}

	public async Task<bool> AddAsync(string userId, BookAddFormModel model)
	{
		var newBook = this.CreateNewBook(userId, model);

		bool bookExists = await this.BookExistsAsync(newBook);

		if (bookExists)
		{
			return false;
		}

		await this._dbContext
			.Books
			.AddAsync(newBook);

		await this._dbContext
			.SaveChangesAsync();

		return true;
	}

	public async Task<IEnumerable<BooksMineViewModel>> MineAsync(string userId)
	{
		return await this._dbContext
			.Books
			.Where(b => b.UsersBooks.Any(ub => ub.CollectorId == userId))
			.Select(b => new BooksMineViewModel
			{
				Id = b.Id,
				Title = b.Title,
				Author = b.Author,
				Description = b.Description,
				ImageUrl = b.ImageUrl,
				Category = b.Category.Name
			})
			.AsNoTracking()
			.ToArrayAsync();
	}

	public async Task<bool> RemoveFromCollectionAsync(string userId, int id)
	{
		var bookToRemove = await this._dbContext
			.IdentityUserBooks
			.FirstOrDefaultAsync(iub => iub.CollectorId == userId && iub.BookId == id);

		if (bookToRemove == null)
		{
			return false;
		}

		this._dbContext
			.IdentityUserBooks
			.Remove(bookToRemove);

		await this._dbContext.SaveChangesAsync();

		return true;
	}
}