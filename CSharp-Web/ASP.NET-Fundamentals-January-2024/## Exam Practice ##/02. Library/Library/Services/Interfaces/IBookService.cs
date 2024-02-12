namespace Library.Services.Interfaces;

using Models;

public interface IBookService
{
	Task<IEnumerable<BooksAllViewModel>> AllBooksAsync();

	Task<bool> AddBookToCollectionAsync(string userId, int id);

	Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();

	Task<bool> AddAsync(string userId, BookAddFormModel model);
}