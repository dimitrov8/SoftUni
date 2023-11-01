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

        // TODO ->

        // 11. Count Books 
        //string result = CountBooks(dbContext, 12);
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
        //string result = IncreasePrices(dbContext);
        //Console.WriteLine(result);

        // 16. Remove Books 
        //string result = RemoveBooks(dbContext);
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
    {
        throw new NotImplementedException();
    }

    // 12. Total Book Copies 
    public static string CountCopiesByAuthor(BookShopContext context)
    {
        throw new NotImplementedException();
    }

    // 13. Profit by Category 
    public static string GetTotalProfitByCategory(BookShopContext context)
    {
        throw new NotImplementedException();
    }

    // 14. Most Recent Books 
    public static string GetMostRecentBooks(BookShopContext context)
    {
        throw new NotImplementedException();
    }

    // 15. Increase Prices
    public static void IncreasePrices(BookShopContext context)
    {
        throw new NotImplementedException();
    }

    // 16. Remove Books 
    public static int RemoveBooks(BookShopContext context)
    {
        throw new NotImplementedException();
    }
}