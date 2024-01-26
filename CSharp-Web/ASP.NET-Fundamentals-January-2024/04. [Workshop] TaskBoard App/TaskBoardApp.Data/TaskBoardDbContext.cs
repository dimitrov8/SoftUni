namespace TaskBoardApp.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class TaskBoardDbContext : IdentityDbContext
{
	public TaskBoardDbContext(DbContextOptions<TaskBoardDbContext> options)
		: base(options)
	{
	}
}