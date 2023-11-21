namespace Invoices.Data.Models;

using System.ComponentModel.DataAnnotations.Schema;

public class ProductClient
{
    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;
}