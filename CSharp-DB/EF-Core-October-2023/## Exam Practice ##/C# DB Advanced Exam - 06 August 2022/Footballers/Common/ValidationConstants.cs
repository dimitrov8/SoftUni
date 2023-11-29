namespace Footballers.Common;

public static class ValidationConstants
{
    // Footballer
    public const int FOOTBALLER_NAME_MIN_LENGTH = 2;
    public const int FOOTBALLER_NAME_MAX_LENGTH = 40;

    // Team
    public const string TEAM_NAME_REGEX = @"[A-Za-z\d\.\- ]{3,40}";

    public const int TEAM_NATIONALITY_MIN_LENGTH = 2;
    public const int TEAM_NATIONALITY_MAX_LENGTH = 40;

    // Coach 
    public const int COACH_NAME_MIN_LENGTH = 2;
    public const int COACH_NAME_MAX_LENGTH = 40;

}