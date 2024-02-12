namespace Library.Data.Models;

using System.ComponentModel.DataAnnotations;
using static Common.EntityValidations.Category;

public class Category
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(MAX_NAME_LENGTH)]
	public string Name { get; set; } = null!;

	public ICollection<Book> Books = new HashSet<Book>();
}