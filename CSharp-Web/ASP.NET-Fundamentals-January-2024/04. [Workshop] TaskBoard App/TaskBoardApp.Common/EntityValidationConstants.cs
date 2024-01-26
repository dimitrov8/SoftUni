namespace TaskBoardApp.Common;

public static class EntityValidationConstants
{
	public static class Task
	{
		public const int MIN_TITLE_LENGTH = 5;
		public const int MAX_TITLE_LENGTH = 70;

		public const int MIN_DESCRIPTION_LENGTH = 10;
		public const int MAX_DESCRIPTION_LENGTH = 1000;
	}

	public static class Board
	{
		public const int MIN_BOARD_NAME = 3;
		public const int MAX_BOARD_NAME = 30;
	}
}