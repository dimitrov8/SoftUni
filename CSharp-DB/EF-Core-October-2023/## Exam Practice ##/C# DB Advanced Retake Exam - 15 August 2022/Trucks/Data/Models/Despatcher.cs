namespace Trucks.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class Despatcher
{
    public Despatcher()
    {
        this.Trucks = new HashSet<Truck>();
    }

    [Key]
    public int Id { get; set; }

    [MinLength(ValidationConstants.DESPATCHER_NAME_MIN_LENGTH)]
    [MaxLength(ValidationConstants.DESPATCHER_NAME_MAX_LENGTH)]
    [Required]
    public string Name { get; set; } = null!;

    public string? Position { get; set; }

    public virtual ICollection<Truck> Trucks { get; set; }
}