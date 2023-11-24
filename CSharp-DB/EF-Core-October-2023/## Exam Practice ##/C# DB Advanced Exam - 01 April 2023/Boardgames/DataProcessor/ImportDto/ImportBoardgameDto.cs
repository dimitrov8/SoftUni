namespace Boardgames.DataProcessor.ImportDto;

using Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

[XmlType("Boardgame")]
public class ImportBoardgameDto
{
    [Required]
    [XmlElement("Name")]
    [MinLength(ValidationConstants.MIN_BOARD_GAME_LENGTH)]
    [MaxLength(ValidationConstants.MAX_BOARD_GAME_LENGTH)]
    public string Name { get; set; } = null!;

    [Required]
    [XmlElement("Rating")]
    [Range(ValidationConstants.MIN_BOARD_GAME_RATING, ValidationConstants.MAX_BOARD_GAME_RATING)]
    public double Rating { get; set; }

    [Required]
    [XmlElement("YearPublished")]
    [Range(ValidationConstants.MIN_BOARD_GAME_YEAR, ValidationConstants.MAX_BOARD_GAME_YEAR)]
    public int YearPublished { get; set; }

    [Required]
    [XmlElement("CategoryType")]
    public int CategoryType { get; set; }

    [Required]
    [XmlElement("Mechanics")]
    public string Mechanics { get; set; } = null!;
}