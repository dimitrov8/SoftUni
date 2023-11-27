namespace Trucks.DataProcessor.ImportDto;

using Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

[XmlType("Despatcher")]
public class ImportDespatcherDto
{
    [XmlElement("Name")]
    [MinLength(ValidationConstants.DESPATCHER_NAME_MIN_LENGTH)]
    [MaxLength(ValidationConstants.DESPATCHER_NAME_MAX_LENGTH)]
    [Required]
    public string Name { get; set; } = null!;

    [XmlElement("Position")]
    [Required]
    public string Position { get; set; } = null!;

    [XmlArray("Trucks")]
    public ImportTruckDto[] Trucks { get; set; }
}