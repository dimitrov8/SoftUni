namespace Invoices.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Invoices.Common;

public class Address
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(ValidationConstants.MAX_STREET_NAME_LENGTH, MinimumLength = ValidationConstants.MIN_STREET_NAME_LENGTH)]
    public string StreetName { get; set; } = null!;

    [Required]
    public int StreetNumber { get; set; }

    [Required]
    public string PostCode { get; set; } = null!;

    [Required]
    [StringLength(ValidationConstants.MAX_CITY_NAME_LENGTH, MinimumLength = ValidationConstants.MIN_CITY_NAME_LENGTH)]
    public string City { get; set; } = null!;

    [Required]
    [StringLength(ValidationConstants.MAX_COUNTRY_NAME_LENGTH, MinimumLength = ValidationConstants.MIN_COUNTRY_NAME_LENGTH)]
    public string Country { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;
}