namespace TaskBoardApp.Services;

using System.Security.Claims;
using Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Board;
using Web.ViewModels.Task;

public class TaskService : ITaskService
{
	private readonly TaskBoardAppDbContext _dbContext;

	public TaskService(TaskBoardAppDbContext dbContext)
	{
		this._dbContext = dbContext;
	}

	private async Task<IEnumerable<BoardSelectViewModel>> GetBoards()
	{
		IEnumerable<BoardSelectViewModel> allBoards = await this._dbContext
			.Boards
			.Select(b => new BoardSelectViewModel
			{
				Id = b.Id,
				Name = b.Name
			})
			.AsNoTracking()
			.ToArrayAsync();

		return allBoards;
	}

	public async Task AddAsync(string ownerId, TaskFormModel viewModel)
	{
		var task = new Data.Models.Task
		{
			Title = viewModel.Title,
			Description = viewModel.Description,
			BoardId = viewModel.BoardId,
			CreatedOn = DateTime.UtcNow,
			OwnerId = ownerId
		};

		await this._dbContext.Tasks.AddAsync(task);
		await this._dbContext.SaveChangesAsync();
	}

	public async Task<TaskDetailsViewModel> GetDetailsByIdAsync(string id)
	{
		var viewModel = await this._dbContext
			.Tasks
			.Select(t => new TaskDetailsViewModel
			{
				Id = t.Id.ToString(),
				Title = t.Title,
				Description = t.Description,
				Owner = t.Owner.UserName,
				CreatedOn = t.CreatedOn.ToString("f"),
				Board = t.Board.Name
			})
			.AsNoTracking()
			.FirstAsync(t => t.Id == id);

		return viewModel;
	}

	public async Task<TaskFormModel?> GetForEditAsync(string id)
	{
		var viewModel = await this._dbContext
			.Tasks
			.Where(t => t.Id.ToString() == id)
			.Select(t => new TaskFormModel
			{
				Title = t.Title,
				Description = t.Description,
				BoardId = t.BoardId
			})
			.AsNoTracking()
			.FirstOrDefaultAsync();

		if (viewModel != null)
		{
			viewModel.Boards = await this.GetBoards();
		}

		return viewModel;
	}

	public async Task EditAsync(string id, TaskFormModel model)
	{
		var task = await this._dbContext
			.Tasks
			.FirstOrDefaultAsync(t => t.Id.ToString() == id);

		task.Title = model.Title;
		task.Description = model.Description;
		task.BoardId = model.BoardId;

		await this._dbContext.SaveChangesAsync();
	}

	public async Task<TaskViewModel> GetForDeleteAsync(string id)
	{
		var task = await this._dbContext
			.Tasks
			.FirstOrDefaultAsync(t => t.Id.ToString() == id);

		var viewModel = new TaskViewModel
		{
			Id = task.Id.ToString(),
			Title = task.Title,
			Description = task.Description
		};

		return viewModel;
	}

	public async Task DeleteAsync(TaskViewModel model)
	{
		var task = await this._dbContext
			.Tasks
			.FirstOrDefaultAsync(t => t.Id.ToString() == model.Id);

		this._dbContext.Tasks.Remove(task);
		await this._dbContext.SaveChangesAsync();
	}

	public async Task<int> GetUserTasksCountAsync(ClaimsPrincipal user)
	{
		if (user.Identity?.IsAuthenticated == true)
		{
			string? currentUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			return await this._dbContext.Tasks.CountAsync(t => t.OwnerId == currentUserId);
		}

		return -1;
	}

	public async Task<int> GetAllTasksCountAsync()
	{
		return await this._dbContext.Tasks.CountAsync();
	}
}