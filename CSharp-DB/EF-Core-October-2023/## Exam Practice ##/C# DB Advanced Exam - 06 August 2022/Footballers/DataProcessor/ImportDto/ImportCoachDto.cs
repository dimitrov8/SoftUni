namespace Footballers.DataProcessor.ImportDto;

using Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

[XmlType("Coach")]
public class ImportCoachDto
{
    [XmlElement("Name")]
    [Required]
    [MinLength(ValidationConstants.COACH_NAME_MIN_LENGTH)]
    [MaxLength(ValidationConstants.COACH_NAME_MAX_LENGTH)]
    public string Name { get; set; } = null!;

    [XmlElement("Nationality")]
    [Required]
    public string Nationality { get; set; } = null!;

    [XmlArray("Footballers")]
    public ImportFootballerDto[] Footballers { get; set; } = null!;
}