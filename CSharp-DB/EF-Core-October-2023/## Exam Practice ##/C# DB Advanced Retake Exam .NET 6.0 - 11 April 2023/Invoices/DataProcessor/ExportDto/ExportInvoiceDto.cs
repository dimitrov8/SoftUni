namespace Invoices.DataProcessor.ExportDto;

using System.Xml.Serialization;

[XmlType("Invoice")]
public class ExportInvoiceDto
{
    [XmlElement("InvoiceNumber")]
    public int InvoiceNumber { get; set; }

    [XmlElement("InvoiceAmount")]
    public decimal InvoiceAmount { get; set; }

    [XmlIgnore]
    public string IssueDate { get; set; } = null!;

    [XmlElement("DueDate")]
    public string DueDate { get; set; } = null!;

    [XmlElement("Currency")]
    public string Currency { get; set; } = null!;
}