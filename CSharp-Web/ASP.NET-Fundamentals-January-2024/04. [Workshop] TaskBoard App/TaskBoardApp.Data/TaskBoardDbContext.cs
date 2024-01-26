namespace TaskBoardApp.Data;

using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

public class TaskBoardDbContext : IdentityDbContext
{
	public TaskBoardDbContext(DbContextOptions<TaskBoardDbContext> options)
		: base(options)
	{
	}

	public DbSet<Board> Boards { get; set; } = null!;

	public DbSet<Task> Tasks { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(TaskBoardDbContext)) ?? Assembly.GetExecutingAssembly());

		base.OnModelCreating(builder);
	}
}