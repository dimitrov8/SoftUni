namespace Forum.Services;

using Data;
using Data.Models;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using ViewModels.Post;

public class PostService : IPostService
{
	private readonly ForumAppDbContext dbContext;

	public PostService(ForumAppDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task<IEnumerable<PostViewModel>> ListAllAsync()
	{
		PostViewModel[] posts = await this.dbContext
			.Posts
			.Select(p => new PostViewModel
			{
				Id = p.Id.ToString(),
				Title = p.Title,
				Content = p.Content
			}).ToArrayAsync();

		return posts;
	}

	public async Task AddPostAsync(PostFormModel model)
	{
		var newPost = new Post
		{
			Title = model.Title,
			Content = model.Content
		};

		await this.dbContext.Posts.AddAsync(newPost);
		await this.dbContext.SaveChangesAsync();
	}

	public async Task<PostFormModel> GetPostForEditOrDeleteAsync(string id)
	{
		if (Guid.TryParse(id, out var postId))
		{
			var post = await this.dbContext
				.Posts
				.FindAsync(postId);

			if (post != null)
			{
				return new PostFormModel
				{
					Title = post.Title,
					Content = post.Content
				};
			}
		}

		return null;
	}

	public async Task EditByIdAsync(string id, PostFormModel model)
	{
		if (Guid.TryParse(id, out var postId))
		{
			var postToEdit = await this.dbContext
				.Posts
				.FindAsync(postId);

			if (postToEdit != null)
			{
				postToEdit.Title = model.Title;
				postToEdit.Content = model.Content;
			}

			await this.dbContext.SaveChangesAsync();
		}
	}

	public async Task DeleteByIdAsync(string id)
	{
		if (Guid.TryParse(id, out var postId))
		{
			var postToDelete = await this.dbContext
				.Posts
				.FindAsync(postId);

			if (postToDelete != null)
			{
				this.dbContext.Posts.Remove(postToDelete);

				await this.dbContext.SaveChangesAsync();
			}
		}
	}
}