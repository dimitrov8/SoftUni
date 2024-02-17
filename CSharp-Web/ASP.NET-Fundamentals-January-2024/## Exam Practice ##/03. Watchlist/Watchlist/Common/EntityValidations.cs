namespace Watchlist.Common;

public static class EntityValidations
{
	public static class Movie
	{
		public const int MIN_TITLE_LENGTH = 10;
		public const int MAX_TITLE_LENGTH = 50;

		public const int MIN_DIRECTOR_LENGTH = 5;
		public const int MAX_DIRECTOR_LENGTH = 50;

		public const int MIN_RATING_VALUE = 0;
		public const int MAX_RATING_VALUE = 10;
	}

	public static class Genre
	{
		public const int MIN_NAME_LENGTH = 5;
		public const int MAX_NAME_LENGTH = 50;
	}
}