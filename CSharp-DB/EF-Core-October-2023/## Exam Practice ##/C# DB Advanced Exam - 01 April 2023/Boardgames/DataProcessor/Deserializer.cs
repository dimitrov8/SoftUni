namespace Boardgames.DataProcessor;

using Data;
using System.ComponentModel.DataAnnotations;

public class Deserializer
{
    private const string ERROR_MESSAGE = "Invalid data!";

    private const string SUCCESSFULLY_IMPORTED_CREATOR
        = "Successfully imported creator – {0} {1} with {2} boardgames.";

    private const string SUCCESSFULLY_IMPORTED_SELLER
        = "Successfully imported seller - {0} with {1} boardgames.";

    public static string ImportCreators(BoardgamesContext context, string xmlString)
    {
        throw new NotImplementedException();
    }

    public static string ImportSellers(BoardgamesContext context, string jsonString)
    {
        throw new NotImplementedException();
    }

    private static bool IsValid(object dto)
    {
        var validationContext = new ValidationContext(dto);
        var validationResult = new List<ValidationResult>();

        return Validator.TryValidateObject(dto, validationContext, validationResult, true);
    }
}