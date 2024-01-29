﻿namespace TaskBoardApp.Services.Interfaces;

using Web.ViewModels.Task;

public interface ITaskService

{
	Task AddAsync(string ownerId, TaskFormModel viewModel);

	Task<TaskDetailsViewModel> GetDetailsByIdAsync(string id);
}