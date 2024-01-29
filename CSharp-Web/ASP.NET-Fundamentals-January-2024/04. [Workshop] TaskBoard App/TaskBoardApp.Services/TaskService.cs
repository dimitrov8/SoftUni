namespace TaskBoardApp.Services;

using Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Task;

public class TaskService : ITaskService
{
	private readonly TaskBoardAppDbContext _dbContext;

	public TaskService(TaskBoardAppDbContext dbContext)
	{
		this._dbContext = dbContext;
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
			.FirstAsync(t => t.Id == id);

		return viewModel;
	}
}