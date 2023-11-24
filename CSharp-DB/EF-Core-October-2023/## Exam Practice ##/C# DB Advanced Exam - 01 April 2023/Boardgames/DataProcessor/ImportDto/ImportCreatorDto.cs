namespace Boardgames.DataProcessor.ImportDto;

using Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

[XmlType("Creator")]
public class ImportCreatorDto
{
    [Required]
    [XmlElement("FirstName")]
    [MinLength(ValidationConstants.MIN_CREATOR_FIRST_NAME_LENGTH)]
    [MaxLength(ValidationConstants.MAX_CREATOR_FIRST_NAME_LENGTH)]
    public string FirstName { get; set; } = null!;

    [Required]
    [XmlElement("LastName")]
    [MinLength(ValidationConstants.MIN_CREATOR_LAST_NAME_LENGTH)]
    [MaxLength(ValidationConstants.MAX_CREATOR_LAST_NAME_LENGTH)]
    public string LastName { get; set; } = null!;

    [XmlArray("Boardgames")]
    public ImportBoardgameDto[] Boardgames { get; set; } = null!;
}