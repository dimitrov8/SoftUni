namespace ForumApp.Controllers;

using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Post;

public class PostController : Controller
{
	private readonly ForumAppDbContext data;

	public PostController(ForumAppDbContext data)
	{
		this.data = data;
	}

	public async Task<IActionResult> All()
	{
		List<PostViewModel> posts = await this.data
			.Posts
			.Select(p => new PostViewModel
			{
				Id = p.Id,
				Title = p.Title,
				Content = p.Content
			}).ToListAsync();

		return this.View(posts);
	}
}