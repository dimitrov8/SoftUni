namespace Forum.App.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using ViewModels.Post;

public class PostController : Controller
{
	private readonly IPostService postService;

	public PostController(IPostService postService)
	{
		this.postService = postService;
	}

	public async Task<IActionResult> All()
	{
		IEnumerable<PostViewModel> posts = await this.postService
			.ListAllAsync();

		return this.View(posts);
	}

	public IActionResult Add()
	{
		return this.View();
	}

	[HttpPost]
	public async Task<IActionResult> Add(PostFormModel model)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(model);
		}

		try
		{
			await this.postService.AddPostAsync(model);
		}
		catch (Exception)
		{
			this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while adding your post!");
			return this.View(model);
		}

		return this.RedirectToAction("All");
	}

	public async Task<IActionResult> Edit(string id)
	{
		try
		{
			var post = await this.postService
				.GetPostForEditOrDeleteAsync(id);

			return this.View(post);
		}
		catch (Exception)
		{
			return this.RedirectToAction("All", "Post");
		}
	}

	[HttpPost]
	public async Task<IActionResult> Edit(string id, PostFormModel model)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(model);
		}

		try
		{
			await this.postService.EditByIdAsync(id, model);
		}
		catch (Exception)
		{
			this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while updating your post!");

			return this.View(model);
		}

		return this.RedirectToAction("All", "Post");
	}

	public async Task<IActionResult> DeleteWithView(string id)
	{
		try
		{
			var model = await this.postService.GetPostForEditOrDeleteAsync(id);

			return this.View(model);
		}
		catch (Exception)
		{
			return this.RedirectToAction("All", "Post");
		}
	}

	[HttpPost]
	public async Task<IActionResult> DeleteWithView(string id, PostFormModel model)
	{
		try
		{
			await this.postService.DeleteByIdAsync(id);
		}
		catch (Exception)
		{
			this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while updating your post!");

			return this.View(model);
		}

		return this.RedirectToAction("All", "Post");
	}

	[HttpPost]
	public async Task<IActionResult> Delete(string id)
	{
		try
		{
			await this.postService.DeleteByIdAsync(id);
		}
		catch (Exception)
		{
			// ignored
		}

		return this.RedirectToAction("All", "Post");
	}
}