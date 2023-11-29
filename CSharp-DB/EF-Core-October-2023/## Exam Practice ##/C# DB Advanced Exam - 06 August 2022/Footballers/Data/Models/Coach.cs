namespace Footballers.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class Coach
{
    public Coach()
    {
        this.Footballers = new HashSet<Footballer>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ValidationConstants.COACH_NAME_MAX_LENGTH)]
    public string Name { get; set; } = null!;

    [Required]
    public string Nationality { get; set; } = null!;

    public virtual ICollection<Footballer> Footballers { get; set; }
}