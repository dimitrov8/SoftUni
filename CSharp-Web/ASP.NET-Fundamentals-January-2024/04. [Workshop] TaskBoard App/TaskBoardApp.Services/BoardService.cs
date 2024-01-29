﻿namespace TaskBoardApp.Services;

using Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Board;
using Web.ViewModels.Task;

public class BoardService : IBoardService
{
	private readonly TaskBoardAppDbContext _dbContext;

	public BoardService(TaskBoardAppDbContext dbContext)
	{
		this._dbContext = dbContext;
	}

	public async Task<IEnumerable<BoardViewModel>> AllAsync()
	{
		IEnumerable<BoardViewModel> allBoards = await this._dbContext
			.Boards
			.Select(b => new BoardViewModel
			{
				Id = b.Id,
				Name = b.Name,
				Tasks = b.Tasks
					.Select(t => new TaskViewModel
					{
						Id = t.Id.ToString(),
						Title = t.Title,
						Description = t.Description,
						Owner = t.Owner.UserName
					})
					.ToArray()
			})
			.AsNoTracking()
			.ToArrayAsync();

		return allBoards;
	}

	public async Task<IEnumerable<BoardSelectViewModel>> AllForSelectAsync()
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

	public async Task<bool> ExistsByIdAsync(int id)
	{
		return await this._dbContext
			.Boards
			.AnyAsync(b => b.Id == id);
	}
}