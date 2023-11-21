namespace Invoices.Data.Models;

using System.ComponentModel.DataAnnotations;
using Invoices.Common;

public class Client
{
    public Client()
    {
        this.Addresses = new HashSet<Address>();
        this.Invoices = new HashSet<Invoice>();
        this.ProductsClients = new HashSet<ProductClient>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(ValidationConstants.MAX_CLIENT_NAME_LENGTH, MinimumLength = ValidationConstants.MIN_CLIENT_NAME_LENGTH)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(ValidationConstants.MAX_CLIENT_NUMBER_VAT_LENGTH, MinimumLength = ValidationConstants.MIN_CLIENT_NUMBER_VAT_LENGTH)]
    public string NumberVat { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; }

    public virtual ICollection<ProductClient> ProductsClients { get; set; }
}