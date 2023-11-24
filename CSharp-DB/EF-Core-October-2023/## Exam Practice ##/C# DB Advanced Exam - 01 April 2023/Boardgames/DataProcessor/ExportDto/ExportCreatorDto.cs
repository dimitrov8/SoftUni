namespace Boardgames.DataProcessor.ExportDto;

using System.Xml.Serialization;

[XmlType("Creator")]
public class ExportCreatorDto
{
    [XmlAttribute("BoardgamesCount")]
    public int BoardgamesCount { get; set; }

    [XmlElement("CreatorName")]
    public string Name { get; set; } = null!;

    [XmlArray("Boardgames")]
    public ExportXmlBoardgameDto[] Boardgames { get; set; } = null!;
}