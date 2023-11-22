namespace Invoices.DataProcessor.ImportDto;

using Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

[XmlType("Address")]
public class AddressDto
{
    [XmlElement("StreetName")]
    [StringLength(ValidationConstants.MAX_STREET_NAME_LENGTH, MinimumLength = ValidationConstants.MIN_STREET_NAME_LENGTH)]
    [Required]
    public string StreetName { get; set; } = null!;

    [XmlElement("StreetNumber")]
    [Required]
    public int StreetNumber { get; set; }

    [XmlElement("PostCode")]
    [Required]
    public string PostCode { get; set; } = null!;

    [XmlElement("City")]
    [StringLength(ValidationConstants.MAX_CITY_NAME_LENGTH, MinimumLength = ValidationConstants.MIN_CITY_NAME_LENGTH)]
    [Required]
    public string City { get; set; } = null!;

    [XmlElement("Country")]
    [StringLength(ValidationConstants.MAX_COUNTRY_NAME_LENGTH, MinimumLength = ValidationConstants.MIN_COUNTRY_NAME_LENGTH)]
    [Required]
    public string Country { get; set; } = null!;
}