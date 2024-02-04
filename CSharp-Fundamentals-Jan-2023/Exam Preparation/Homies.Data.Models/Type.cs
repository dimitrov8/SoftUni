namespace Homies.Data.Models;

using System.ComponentModel.DataAnnotations;
using static Homies.Common.EntityValidations.Type;

public class Type
{
	public Type()
	{
		this.Events = new HashSet<Event>();
	}

	[Key]
	public int Id { get; init; }

	[Required]
	[MaxLength(NAME_MAX_LENGTH)]
	public string Name { get; init; } = null!;

	public ICollection<Event> Events { get; set; }
}