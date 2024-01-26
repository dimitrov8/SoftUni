namespace TaskBoardApp.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

internal class BoardEntityConfiguration : IEntityTypeConfiguration<Board>
{
	public void Configure(EntityTypeBuilder<Board> builder)
	{
		ICollection<Board> boards = this.GenerateBoards();

		builder.HasData(boards);
	}

	private ICollection<Board> GenerateBoards()
	{
		ICollection<Board> boards = new HashSet<Board>
		{
			new()
			{
				Id = 1,
				Name = "Open"
			},
			new()
			{
				Id = 2,
				Name = "In Progress"
			},
			new()
			{
				Id = 3,
				Name = "Done"
			}
		};

		return boards;
	}
}