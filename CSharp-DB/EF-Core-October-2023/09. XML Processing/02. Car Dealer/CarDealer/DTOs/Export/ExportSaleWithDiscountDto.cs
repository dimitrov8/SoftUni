namespace CarDealer.DTOs.Export;

using System.Xml.Serialization;

[XmlType("sale")]
public class ExportSaleWithDiscountDto
{
    [XmlElement("car")]
    public ExportCarAttributeDto Car { get; set; } = null!;

    [XmlElement("discount")]
    public decimal Discount { get; set; }

    [XmlElement("customer-name")]
    public string CustomerName { get; set; } = null!;

    [XmlElement("price")]
    public decimal Price { get; set; }

    [XmlElement("price-with-discount")]
    public decimal PriceWithDiscount

    {
        get
        {
            if (this.IsYoungDriver)
            {
                return this.Price;
            }

            return this.Price - this.Price * this.Discount / 100;
        }
        set { }
    }

    [XmlIgnore]
    public bool IsYoungDriver { get; set; }
}