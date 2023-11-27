namespace Trucks.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ClientTruck
{
    [ForeignKey(nameof(Client))]
    [Required]
    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;

    [ForeignKey(nameof(Truck))]
    [Required]
    public int TruckId { get; set; }

    public virtual Truck Truck { get; set; } = null!;
}