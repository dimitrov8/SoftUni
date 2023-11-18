namespace ProductShop.DTOs.Export;

using System.Xml.Serialization;

[XmlType("SoldProducts")]
public class ExportSoldProductCountDto
{
    [XmlElement("count")]
    public int Count { get; set; }

    [XmlArray("products")]
    public ExportSoldProductDto[] Products { get; set; } = null!;
}