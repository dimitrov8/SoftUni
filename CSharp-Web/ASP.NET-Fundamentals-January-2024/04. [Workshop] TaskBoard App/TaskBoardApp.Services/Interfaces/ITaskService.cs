namespace TaskBoardApp.Services.Interfaces;

using System.Security.Claims;
using Web.ViewModels.Task;

public interface ITaskService

{
	Task AddAsync(string ownerId, TaskFormModel viewModel);

	Task<TaskDetailsViewModel> GetDetailsByIdAsync(string id);

	Task<TaskFormModel?> GetForEditAsync(string id);

	Task EditAsync(string id, TaskFormModel model);

	Task<TaskViewModel> GetForDeleteAsync(string id);

	Task DeleteAsync(TaskViewModel model);

	Task<int> GetUserTasksCountAsync(ClaimsPrincipal user);

	Task<int> GetAllTasksCountAsync();
}