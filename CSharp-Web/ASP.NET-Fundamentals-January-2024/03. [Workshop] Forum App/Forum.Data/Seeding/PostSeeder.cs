namespace Forum.Data.Seeding;

using Models;

internal class PostSeeder
{
	internal Post[] GeneratePosts()
	{
		ICollection<Post> posts = new HashSet<Post>
		{
			new()
			{
				Title = "My first post",
				Content = "My first post will be about performing CRUD operations in MVC. It's so much fun!"
			},
			new()
			{
				Title = "My second post",
				Content = "This is my second post. CRUD operations in MVC are getting more and more interesting!"
			},
			new()
			{
				Title = "My third post",
				Content = "Hello there! I'm getting better and better with the CRUD operations in MVC. Stay tuned!"
			}
		};

		return posts.ToArray();
	}
}