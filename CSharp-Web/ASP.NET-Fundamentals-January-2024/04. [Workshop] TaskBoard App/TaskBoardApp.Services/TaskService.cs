namespace TaskBoardApp.Services;

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
}