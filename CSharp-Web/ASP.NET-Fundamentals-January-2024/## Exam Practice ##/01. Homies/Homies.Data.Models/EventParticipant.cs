namespace Homies.Data.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class EventParticipant
{
	[ForeignKey(nameof(Helper))]
	public string HelperId { get; set; } = null!;

	public IdentityUser Helper { get; set; } = null!;

	[ForeignKey(nameof(Event))]
	public int EventId { get; set; }

	public Event Event { get; set; } = null!;
}