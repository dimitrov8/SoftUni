namespace BookShop;

using Data;
using Initializer;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Enums;
using System.Globalization;
using System.Text;

public class StartUp
{
    public static void Main()
    {
        using var dbContext = new BookShopContext();
        DbInitializer.ResetDatabase(dbContext);

        // 02. Age Restriction 
        //string result = GetBooksByAgeRestriction(dbContext, "teEn");
        //Console.WriteLine(result);

        // 03. Golden Books
        //string result = GetGoldenBooks(dbContext);
        //Console.WriteLine(result);

        // 04. Books by Price 
        //string result = GetBooksByPrice(dbContext);
        //Console.WriteLine(result);

        // 05. Not Released In
        //string result = GetBooksNotReleasedIn(dbContext, 2000);
        //Console.WriteLine(result);

        // 06. Book Titles by Category 
        //string result = GetBooksByCategory(dbContext, "horror mystery drama");
        //Console.WriteLine(result);

        // 07. Released Before Date 
        //string result = GetBooksReleasedBefore(dbContext, "12-04-1992");
        //Console.WriteLine(result);

        // 08. Author Search
        //string result = GetAuthorNamesEndingIn(dbContext, "e");
        //Console.WriteLine(result);

        // 09. Book Search
        //string result = GetBookTitlesContaining(dbContext, "sk");
        //Console.WriteLine(result);

        // 10. Book Search by Author 
        //string result = GetBooksByAuthor(dbContext, "R");
        //Console.WriteLine(result);

        //11.Count Books
        //int result = CountBooks(dbContext, 12);
        //Console.WriteLine(result);

        // 12. Total Book Copies 
        //string result = CountCopiesByAuthor(dbContext);
        //Console.WriteLine(result);

        // 13. Profit by Category 
        //string result = GetTotalProfitByCategory(dbContext);
        //Console.WriteLine(result);

        // 14. Most Recent Books 
        //string result = GetMostRecentBooks(dbContext);
        //Console.WriteLine(result);

        // 15. Increase Prices
        //IncreasePrices(dbContext);

        // 16. Remove Books 
        //int result = RemoveBooks(dbContext);
        //Console.WriteLine(result);
    }

    // 02. Age Restriction 
    public static string GetBooksByAgeRestriction(BookShopContext context, string command)
    {
        var currentAgeRestriction = Enum.Parse<AgeRestriction>(command, true);

        string[] bookTitles = context.Books
            .Where(b => b.AgeRestriction == currentAgeRestriction)
            .Select(b => b.Title)
            .OrderBy(title => title)
            .AsNoTracking()
            .ToArray();

        return string.Join(Environment.NewLine, bookTitles);
    }

    // 03. Golden Books
    public static string GetGoldenBooks(BookShopContext context)
    {
        string[] goldenBooks = context.Books
            .Where(b => b.EditionType == Enum.Parse<EditionType>("Gold", true) && b.Copies < 5000)
            .OrderBy(b => b.BookId)
            .Select(b => b.Title)
            .AsNoTracking()
            .ToArray();

        return string.Join(Environment.NewLine, goldenBooks);
    }

    // 04. Books by Price 
    public static string GetBooksByPrice(BookShopContext context)
    {
        var books = context.Books
            .Where(b => b.Price > 40)
            .OrderByDescending(b => b.Price)
            .Select(b => new
            {
                b.Title,
                Price = b.Price.ToString("F2")
            })
            .AsNoTracking()
            .ToArray();

        var sb = new StringBuilder();

        foreach (var b in books)
        {
            sb.AppendLine($"{b.Title} - ${b.Price}");
        }

        return sb.ToString().TrimEnd();
    }

    // 05. Not Released In
    public static string GetBooksNotReleasedIn(BookShopContext context, int year)
    {
        string[] books = context.Books
            .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year != year)
            .OrderBy(b => b.BookId)
            .Select(b => b.Title)
            .AsNoTracking()
            .ToArray();

        return string.Join(Environment.NewLine, books);
    }

    // 06. Book Titles by Category
    public static string GetBooksByCategory(BookShopContext context, string input)
    {
        string[] desiredBookCategories = input
            .ToLower()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries);

        int[] desiredBookCategoriesIds = context.Categories
            .Where(c => desiredBookCategories.Contains(c.Name.ToLower()))
            .AsNoTracking()
            .Select(c => c.CategoryId)
            .ToArray();

        Book[] books = context.BooksCategories
            .Where(bc => desiredBookCategoriesIds.Contains(bc.CategoryId))
            .Select(b => b.Book)
            .OrderBy(b => b.Title)
            .AsNoTracking()
            .ToArray();

        var sb = new StringBuilder();

        foreach (var b in books)
        {
            sb.AppendLine(b.Title);
        }

        return sb.ToString().TrimEnd();
    }

    // 07. Released Before Date 
    public static string GetBooksReleasedBefore(BookShopContext context, string date)
    {
        var dateConverted = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        var books = context.Books
            .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value < dateConverted)
            .OrderByDescending(b => b.ReleaseDate)
            .Select(b => new
            {
                b.Title,
                Edition = b.EditionType.ToString(),
                Price = b.Price.ToString("F2")
            })
            .AsNoTracking()
            .ToArray();

        var sb = new StringBuilder();

        foreach (var b in books)
        {
            sb.AppendLine($"{b.Title} - {b.Edition} - ${b.Price}");
        }

        return sb.ToString().TrimEnd();
    }

    // 08. Author Search 
    public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
    {
        var authors = context.Authors
            .Where(a => a.FirstName.EndsWith(input))
            .AsNoTracking()
            .ToArray()
            .Select(a => new
            {
                FullName = $"{a.FirstName} {a.LastName}"
            })
            .OrderBy(a => a.FullName)
            .ToArray();

        return string.Join(Environment.NewLine, authors.Select(a => a.FullName));
    }

    // 09. Book Search
    public static string GetBookTitlesContaining(BookShopContext context, string input)
    {
        string[] booksTitles = context.Books
            .Where(b => b.Title.ToLower().Contains(input.ToLower()))
            .Select(b => b.Title)
            .OrderBy(title => title)
            .AsNoTracking()
            .ToArray();

        return string.Join(Environment.NewLine, booksTitles);
    }

    // 10. Book Search by Author 
    public static string GetBooksByAuthor(BookShopContext context, string input)
    {
        var books = context.Books
            .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
            .OrderBy(b => b.BookId)
            .Select(b => new
            {
                b.Title,
                AuthorFullName = $"{b.Author.FirstName} {b.Author.LastName}"
            })
            .AsNoTracking()
            .ToArray();

        return string.Join(Environment.NewLine, books.Select(b => $"{b.Title} ({b.AuthorFullName})"));
    }

    // 11. Count Books 
    public static int CountBooks(BookShopContext context, int lengthCheck)
        => context.Books
            .Where(b => b.Title.Length > lengthCheck)
            .Select(b => b.Title)
            .AsNoTracking()
            .Count();

    // 12. Total Book Copies 
    public static string CountCopiesByAuthor(BookShopContext context)
    {
        var authorBookCopiesCount = context.Authors
            .Select(a => new
            {
                AuthorFullName = $"{a.FirstName} {a.LastName}",
                TotalBookCopies = a.Books.Sum(b => b.Copies)
            })
            .AsNoTracking()
            .OrderByDescending(a => a.TotalBookCopies)
            .ToArray();

        return string.Join(Environment.NewLine, authorBookCopiesCount.Select(a => $"{a.AuthorFullName} - {a.TotalBookCopies}"));
    }

    // 13. Profit by Category 
    public static string GetTotalProfitByCategory(BookShopContext context)
    {
        var bookCategoriesProfits = context.Categories
            .Select(c => new
            {
                c.Name,
                Profit = c.CategoryBooks
                    .Sum(cb => cb.Book.Copies * cb.Book.Price)
            })
            .AsNoTracking()
            .OrderByDescending(bcp => bcp.Profit)
            .ThenBy(bcp => bcp.Name)
            .ToArray()
            .Select(bcp => new
            {
                bcp.Name,
                Profit = bcp.Profit.ToString("F2")
            })
            .ToArray();

        return string.Join(Environment.NewLine, bookCategoriesProfits.Select(bcp => $"{bcp.Name} ${bcp.Profit}"));
    }

    // 14. Most Recent Books 
    public static string GetMostRecentBooks(BookShopContext context)
    {
        var mostRecentBooksByCategories = context.Categories
            .OrderBy(c => c.Name)
            .Select(c => new
            {
                CategoryName = c.Name,
                MostRecentBooks = c.CategoryBooks
                    .OrderByDescending(cb => cb.Book.ReleaseDate)
                    .Take(3)
                    .Select(cb => new
                    {
                        BookTitle = cb.Book.Title,
                        ReleaseDate = cb.Book.ReleaseDate.HasValue
                            ? $"({cb.Book.ReleaseDate.Value.Year})"
                            : "Unknown Release Date"
                    })
                    .ToArray()
            })
            .AsNoTracking()
            .ToArray();

        var sb = new StringBuilder();

        foreach (var c in mostRecentBooksByCategories)
        {
            sb.AppendLine($"--{c.CategoryName}");

            foreach (var b in c.MostRecentBooks)
            {
                sb.AppendLine($"{b.BookTitle} {b.ReleaseDate}");
            }
        }

        return sb.ToString().TrimEnd();
    }

    // 15. Increase Prices
    public static void IncreasePrices(BookShopContext context)
    {
        Book[] booksReleasedBefore2010 = context.Books
            .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year < 2010)
            .ToArray();

        foreach (var b in booksReleasedBefore2010)
        {
            b.Price += 5;
        }

        context.BulkUpdate(booksReleasedBefore2010);
    }

    // 16. Remove Books 
    public static int RemoveBooks(BookShopContext context)
    {
        Book[] booksToRemove = context.Books
            .Where(b => b.Copies < 4200)
            .ToArray();

        context.Books.RemoveRange(booksToRemove);

        context.BulkSaveChanges();

        return booksToRemove.Length;
    }
}