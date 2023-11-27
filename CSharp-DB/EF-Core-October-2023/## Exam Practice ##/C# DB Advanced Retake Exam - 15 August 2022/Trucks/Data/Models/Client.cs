namespace Trucks.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class Client
{
    public Client()
    {
        this.ClientsTrucks = new HashSet<ClientTruck>();
    }

    [Key]
    public int Id { get; set; }

    [MinLength(ValidationConstants.CLIENT_NAME_MIN_LENGTH)]
    [MaxLength(ValidationConstants.CLIENT_NAME_MAX_LENGTH)]
    [Required]
    public string Name { get; set; } = null!;

    [MinLength(ValidationConstants.CLIENT_NATIONALITY_MIN_LENGTH)]
    [MaxLength(ValidationConstants.CLIENT_NATIONALITY_MAX_LENGTH)]
    [Required]
    public string Nationality { get; set; } = null!;

    [Required]
    public string Type { get; set; } = null!;

    public virtual ICollection<ClientTruck> ClientsTrucks { get; set; }
}