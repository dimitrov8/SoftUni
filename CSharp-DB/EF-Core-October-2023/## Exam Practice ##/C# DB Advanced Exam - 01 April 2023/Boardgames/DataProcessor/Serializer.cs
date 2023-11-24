namespace Boardgames.DataProcessor;

using Data;
using ExportDto;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Utilities;

public class Serializer
{
    public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
    {
        var xmlHelper = new XmlHelper();

        ExportCreatorDto[] creators = context.Creators
            .Where(c => c.Boardgames.Any())
            .ToArray()
            .Select(c => new ExportCreatorDto
            {
                BoardgamesCount = c.Boardgames.Count,
                Name = $"{c.FirstName} {c.LastName}",
                Boardgames = c.Boardgames
                    .Select(bg => new ExportXmlBoardgameDto
                    {
                        Name = bg.Name,
                        YearPublished = bg.YearPublished
                    })
                    .OrderBy(bg => bg.Name)
                    .ToArray()
            })
            .OrderByDescending(c => c.BoardgamesCount)
            .ThenBy(c => c.Name)
            .ToArray();

        return xmlHelper.Serialize(creators, "Creators");
    }

    public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
    {
        ExportSellerDto[] sellers = context.Sellers
            .Where(s => s.BoardgamesSellers.Any(bs => bs.Boardgame.YearPublished >= year &&
                                                      bs.Boardgame.Rating <= rating))
            .Select(s => new ExportSellerDto
            {
                Name = s.Name,
                Website = s.Website,
                Boardgames = s.BoardgamesSellers
                    .Where(bs => bs.Boardgame.YearPublished >= year
                                 && bs.Boardgame.Rating <= rating)
                    .Select(bs => new ExportBoardgameDto
                    {
                        Name = bs.Boardgame.Name,
                        Rating = bs.Boardgame.Rating,
                        Mechanics = bs.Boardgame.Mechanics,
                        Category = bs.Boardgame.CategoryType.ToString()
                    })
                    .OrderByDescending(bg => bg.Rating)
                    .ThenBy(bg => bg.Name)
                    .ToArray()
            })
            .AsNoTracking()
            .OrderByDescending(s => s.Boardgames.Length)
            .ThenBy(s => s.Name)
            .Take(5)
            .ToArray();

        return JsonConvert.SerializeObject(sellers, Formatting.Indented);
    }
}