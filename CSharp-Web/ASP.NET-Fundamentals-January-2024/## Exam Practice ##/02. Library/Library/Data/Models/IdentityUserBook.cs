namespace Library.Data.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

public class IdentityUserBook
{
	[Required]
	public string CollectorId { get; set; } = null!;

	[ForeignKey(nameof(CollectorId))]
	public IdentityUser Collector { get; set; } = null!;

	[Required]
	public int BookId { get; set; }

	[ForeignKey(nameof(BookId))]
	public Book Book { get; set; } = null!;
}