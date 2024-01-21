﻿namespace Forum.Common.Validations;

public static class EntityValidations
{
	public static class Post
	{
		public const int POST_TITLE_MAX_LENGTH = 50;
		public const int POST_TITLE_MIN_LENGTH = 10;

		public const int POST_CONTENT_MAX_LENGTH = 1500;
		public const int POST_CONTENT_MIN_LENGTH = 10;
	}
}