namespace Library.Common;

public static class EntityValidations
{
	public static class Book
	{
		public const int MIN_TITLE_LENGTH = 10;
		public const int MAX_TITLE_LENGTH = 50;

		public const int MIN_AUTHOR_LENGTH = 5;
		public const int MAX_AUTHOR_LENGTH = 50;

		public const int MIN_DESCRIPTION_LENGTH = 5;
		public const int MAX_DESCRIPTION_LENGTH = 5000;

		public const int MIN_RATING_VALUE = 0;
		public const int MAX_RATING_VALUE = 10;
	}

	public static class Category
	{
		public const int MIN_NAME_LENGTH = 5;
		public const int MAX_NAME_LENGTH = 50;

		public const int MIN_CATEGORY_VALUE = 1;
		public const int MAX_CATEGORY_VALUE = int.MaxValue;
	}
}