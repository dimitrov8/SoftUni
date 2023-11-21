namespace Invoices.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Address
{
    [Key]
    public int Id { get; set; }

    [StringLength(ValidationConstants.MAX_STREET_NAME_LENGTH, MinimumLength = ValidationConstants.MIN_STREET_NAME_LENGTH)]
    public string StreetName { get; set; } = null!;

    public int StreetNumber { get; set; }

    public string PostCode { get; set; } = null!;

    [StringLength(ValidationConstants.MAX_CITY_NAME_LENGTH, MinimumLength = ValidationConstants.MIN_CITY_NAME_LENGTH)]
    public string City { get; set; } = null!;

    [StringLength(ValidationConstants.MAX_COUNTRY_NAME_LENGTH, MinimumLength = ValidationConstants.MIN_COUNTRY_NAME_LENGTH)]
    public string Country { get; set; } = null!;

    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }

    public Client Client { get; set; } = null!;
}