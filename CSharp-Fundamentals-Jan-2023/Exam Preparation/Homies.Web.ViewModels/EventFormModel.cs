namespace Homies.Web.ViewModels;

using System.ComponentModel.DataAnnotations;
using static Common.EntityValidations.Event;

public class EventFormModel
{
	[Required]
	[StringLength(NAME_MAX_LENGTH, MinimumLength = NAME_MIN_LENGTH)]
	public string Name { get; set; } = null!;

	[Required]
	[StringLength(DESCRIPTION_MAX_LENGTH, MinimumLength = DESCRIPTION_MIN_LENGTH)]
	public string Description { get; set; } = null!;

	[Required]
	[Display(Name = "Start")]
	public string Start { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:m");

	[Required]
	[Display(Name = "End")]
	public string End { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:m");

	[Required]
	[Display(Name = "Type of event")]
	public int TypeId { get; set; }

	public IEnumerable<EventTypeSelectViewModel>? Types { get; set; }
}