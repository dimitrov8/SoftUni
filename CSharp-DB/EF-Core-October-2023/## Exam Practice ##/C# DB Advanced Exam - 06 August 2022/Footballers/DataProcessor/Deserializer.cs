namespace Footballers.DataProcessor;

using Data;
using System.ComponentModel.DataAnnotations;

public class Deserializer
{
    private const string ERROR_MESSAGE = "Invalid data!";

    private const string SUCCESSFULLY_IMPORTED_COACH
        = "Successfully imported coach - {0} with {1} footballers.";

    private const string SUCCESSFULLY_IMPORTED_TEAM
        = "Successfully imported team - {0} with {1} footballers.";

    public static string ImportCoaches(FootballersContext context, string xmlString)
    {
        throw new NotImplementedException();
    }

    public static string ImportTeams(FootballersContext context, string jsonString)
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