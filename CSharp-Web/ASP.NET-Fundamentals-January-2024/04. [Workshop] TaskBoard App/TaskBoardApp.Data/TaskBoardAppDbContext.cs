namespace TaskBoardApp.Data;

using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

public class TaskBoardAppDbContext : IdentityDbContext
{
	public TaskBoardAppDbContext(DbContextOptions<TaskBoardAppDbContext> options)
		: base(options)
	{
	}

	public DbSet<Board> Boards { get; set; } = null!;

	public DbSet<Task> Tasks { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(TaskBoardAppDbContext)) ?? Assembly.GetExecutingAssembly());

		base.OnModelCreating(builder);
	}
}