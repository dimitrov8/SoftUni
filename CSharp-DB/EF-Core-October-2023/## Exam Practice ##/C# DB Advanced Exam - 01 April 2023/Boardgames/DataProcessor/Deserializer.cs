namespace Boardgames.DataProcessor;

using Data;
using Data.Models;
using Data.Models.Enums;
using ImportDto;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Utilities;

public class Deserializer
{
    private const string ERROR_MESSAGE = "Invalid data!";

    private const string SUCCESSFULLY_IMPORTED_CREATOR
        = "Successfully imported creator – {0} {1} with {2} boardgames.";

    private const string SUCCESSFULLY_IMPORTED_SELLER
        = "Successfully imported seller - {0} with {1} boardgames.";

    public static string ImportCreators(BoardgamesContext context, string xmlString)
    {
        var sb = new StringBuilder();
        var xmlHelper = new XmlHelper();

        ImportCreatorDto[] creatorDtos = xmlHelper.Deserialize<ImportCreatorDto[]>(xmlString, "Creators");

        var validCreators = new HashSet<Creator>();

        foreach (var creatorDto in creatorDtos)
        {
            if (!IsValid(creatorDto))
            {
                sb.AppendLine(ERROR_MESSAGE);
                continue;
            }

            var creator = new Creator
            {
                FirstName = creatorDto.FirstName,
                LastName = creatorDto.LastName
            };

            var validBoardgames = new HashSet<Boardgame>();

            foreach (var boardgameDto in creatorDto.Boardgames)
            {
                if (!IsValid(boardgameDto))
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var boardgame = new Boardgame
                {
                    Name = boardgameDto.Name,
                    Rating = boardgameDto.Rating,
                    YearPublished = boardgameDto.YearPublished,
                    CategoryType = (CategoryType)boardgameDto.CategoryType,
                    Mechanics = boardgameDto.Mechanics
                };

                validBoardgames.Add(boardgame);
            }

            creator.Boardgames = validBoardgames;
            validCreators.Add(creator);
            sb.AppendLine(string.Format(SUCCESSFULLY_IMPORTED_CREATOR, creator.FirstName, creator.LastName, creator.Boardgames.Count));
        }

        context.Creators.AddRange(validCreators);
        context.SaveChanges();

        return sb.ToString().TrimEnd();
    }

    public static string ImportSellers(BoardgamesContext context, string jsonString)
    {
        var sb = new StringBuilder();

        ImportSellerDto[] sellerDtos = JsonConvert.DeserializeObject<ImportSellerDto[]>(jsonString);
        int[] existingBoardgameIds = context.Boardgames
            .Select(bg => bg.Id)
            .ToArray();

        var validSellers = new HashSet<Seller>();

        foreach (var sellerDto in sellerDtos)
        {
            if (!IsValid(sellerDto))
            {
                sb.AppendLine(ERROR_MESSAGE);
                continue;
            }

            var seller = new Seller
            {
                Name = sellerDto.Name,
                Address = sellerDto.Address,
                Country = sellerDto.Country,
                Website = sellerDto.Website
            };

            foreach (int boardgameId in sellerDto.Boardgames.Distinct())
            {
                if (!existingBoardgameIds.Contains(boardgameId))
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                seller.BoardgamesSellers.Add(new BoardgameSeller
                {
                    BoardgameId = boardgameId
                });
            }

            validSellers.Add(seller);
            sb.AppendLine(string.Format(SUCCESSFULLY_IMPORTED_SELLER, seller.Name, seller.BoardgamesSellers.Count));
        }

        context.Sellers.AddRange(validSellers);
        context.SaveChanges();

        return sb.ToString().TrimEnd();
    }

    private static bool IsValid(object dto)
    {
        var validationContext = new ValidationContext(dto);
        var validationResult = new List<ValidationResult>();

        return Validator.TryValidateObject(dto, validationContext, validationResult, true);
    }
}