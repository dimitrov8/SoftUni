namespace Invoices.DataProcessor.ImportDto;

using Data;
using Invoices.Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

[XmlType("Client")]
public class ImportClientDto
{
    [XmlElement("Name")]
    [Required]
    [StringLength(ValidationConstants.MAX_CLIENT_NAME_LENGTH, MinimumLength = ValidationConstants.MIN_CLIENT_NAME_LENGTH)]
    public string Name { get; set; } = null!;

    [XmlElement("NumberVat")]
    [Required]
    [StringLength(ValidationConstants.MAX_CLIENT_NUMBER_VAT_LENGTH, MinimumLength = ValidationConstants.MIN_CLIENT_NUMBER_VAT_LENGTH)]
    public string NumberVat { get; set; } = null!;

    [XmlArray("Addresses")]
    public AddressDto[] Addresses { get; set; } = null!;
}