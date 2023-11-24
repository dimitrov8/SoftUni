namespace Boardgames.Common;

using System.Text.RegularExpressions;

public static class ValidationConstants
{
    // Boardgame 
    public const int MIN_BOARD_GAME_LENGTH = 10;
    public const int MAX_BOARD_GAME_LENGTH = 2;

    public const double MIN_BOARD_GAME_RATING = 1.00;
    public const double MAX_BOARD_GAME_RATING = 10.00;

    public const int MIN_BOARD_GAME_YEAR = 2018;
    public const int MAX_BOARD_GAME_YEAR = 2023;

    // Seller
    public const int MIN_SELLER_NAME_LENGTH = 5;
    public const int MAX_SELLER_NAME_LENGTH = 20;

    public const int MIN_SELLER_ADDRESS_LENGTH = 2;
    public const int MAX_SELLER_ADDRESS_LENGTH = 30;

    public const string REGEX_SELLER_WEBSITE = @"^www.[A-Za-z\d-]+.com";

    // Creator
    public const int MIN_CREATOR_FIRST_NAME_LENGTH = 2;
    public const int MAX_CREATOR_FIRST_NAME_LENGTH = 7;
    
    public const int MIN_CREATOR_LAST_NAME_LENGTH = 2;
    public const int MAX_CREATOR_LAST_NAME_LENGTH = 7;

}