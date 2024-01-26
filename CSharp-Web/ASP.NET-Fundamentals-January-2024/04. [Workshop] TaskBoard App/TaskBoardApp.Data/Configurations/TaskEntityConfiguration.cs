namespace TaskBoardApp.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

internal class TaskEntityConfiguration : IEntityTypeConfiguration<Task>
{
	public void Configure(EntityTypeBuilder<Task> builder)
	{
		builder
			.HasOne(t => t.Board)
			.WithMany(b => b.Tasks)
			.HasForeignKey(t => t.BoardId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasData(this.GenerateTasks());
	}

	private ICollection<Task> GenerateTasks()
	{
		var tasks = new HashSet<Task>
		{
			new()
			{
				Title = "Improve CSS styles",
				Description = "Implement better styling for all public pages",
				CreatedOn = DateTime.UtcNow.AddDays(-200),
				OwnerId = "42c27fad-0b2d-4a0d-b431-6a1f166e4cab",
				BoardId = 1
			},
			new()
			{
				Title = "Android Client App",
				Description = "Create Android client App for the RESTful TaskBoard service",
				CreatedOn = DateTime.UtcNow.AddMonths(-5),
				OwnerId = "e35a80d1-b78c-4ad3-b516-7a48608083e5",
				BoardId = 2
			},
			new()
			{
				Title = "Desktop Client App",
				Description = "Create Desktop client App for the RESTful TaskBoard service",
				CreatedOn = DateTime.UtcNow.AddMonths(-1),
				OwnerId = "e35a80d1-b78c-4ad3-b516-7a48608083e5",
				BoardId = 3
			},

			new()
			{
				Title = "Create Tasks",
				Description = "Implement [Create Task] page for adding tasks",
				CreatedOn = DateTime.UtcNow.AddYears(-1),
				OwnerId = "62d4b959-4f43-402f-94a2-0b838a4a539f",
				BoardId = 3
			}
		};

		return tasks;
	}
}