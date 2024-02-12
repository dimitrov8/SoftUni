namespace Homies.Common;

public static class EntityValidations
{
	public static class Event
	{
		public const int NAME_MIN_LENGTH = 5;
		public const int NAME_MAX_LENGTH = 20;

		public const int DESCRIPTION_MIN_LENGTH = 15;
		public const int DESCRIPTION_MAX_LENGTH = 150;
	}

	public static class Type
	{
		public const int NAME_MIN_LENGTH = 5;
		public const int NAME_MAX_LENGTH = 15;
	}
}