namespace ProductShop.DTOs.Export;

using System.Xml.Serialization;

[XmlType("Users")]
public class ExportUserCountAndSoldProductResult
{
    [XmlElement("count")]
    public int Count { get; set; }

    [XmlArray("users")]
    public ExportUserAndSoldProductsCount[] Users { get; set; } = null!;
}