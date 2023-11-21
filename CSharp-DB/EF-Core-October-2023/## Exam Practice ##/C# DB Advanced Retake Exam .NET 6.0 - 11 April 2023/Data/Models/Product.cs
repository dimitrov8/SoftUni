namespace Invoices.Data.Models;

using Enums;
using System.ComponentModel.DataAnnotations;

public class Product
{
    public Product()
    {
        this.ProductsClients = new HashSet<ProductClient>();
    }

    [Key]
    public int Id { get; set; }

    [StringLength(ValidationConstants.MAX_PRODUCT_NAME_LENGTH, MinimumLength = ValidationConstants.MIN_PRODUCT_NAME_LENGTH)]
    public string Name { get; set; } = null!;

    [Range(ValidationConstants.MIN_PRODUCT_PRICE, ValidationConstants.MAX_PRODUCT_PRICE)]
    public decimal Price { get; set; }

    public CategoryType CategoryType { get; set; }

    public ICollection<ProductClient> ProductsClients { get; set; }
}