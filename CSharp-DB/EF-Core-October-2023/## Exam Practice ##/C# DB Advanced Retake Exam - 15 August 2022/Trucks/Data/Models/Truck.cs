namespace Trucks.Data.Models;

using Common;
using Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Truck
{
    public Truck()
    {
        this.ClientsTrucks = new HashSet<ClientTruck>();
    }

    [Key]
    public int Id { get; set; }

    [MinLength(ValidationConstants.TRUCK_REGISTRATION_NUMBER_LENGTH)]
    [MaxLength(ValidationConstants.TRUCK_REGISTRATION_NUMBER_LENGTH)]
    [RegularExpression(ValidationConstants.REGEX_STRING_TRUCK_REGISTRATION_NUMBER)]
    public string? RegistrationNumber { get; set; } 

    [MinLength(ValidationConstants.TRUCK_VIN_NUMBER_LENGTH)]
    [MaxLength(ValidationConstants.TRUCK_VIN_NUMBER_LENGTH)]
    [Required]
    public string VinNumber { get; set; } = null!;

    [Range(ValidationConstants.TRUCK_TANK_CAPACITY_MIN, ValidationConstants.TRUCK_TANK_CAPACITY_MAX)]
    public int TankCapacity { get; set; }

    [Range(ValidationConstants.TRUCK_CARGO_CAPACITY_MIN, ValidationConstants.TRUCK_CARGO_CAPACITY_MAX)]
    public int CargoCapacity { get; set; }

    [Required]
    public CategoryType CategoryType { get; set; }

    [Required]
    public MakeType MakeType { get; set; }

    [ForeignKey(nameof(Despatcher))]
    [Required]
    public int DespatcherId { get; set; }

    public virtual Despatcher Despatcher { get; set; } = null!;

    public virtual ICollection<ClientTruck> ClientsTrucks { get; set; }
}