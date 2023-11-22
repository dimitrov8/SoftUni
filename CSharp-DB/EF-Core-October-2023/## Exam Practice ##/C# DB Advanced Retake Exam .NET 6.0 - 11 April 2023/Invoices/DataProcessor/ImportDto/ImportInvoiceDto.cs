namespace Invoices.DataProcessor.ImportDto;

using Common;
using Data.Models.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

public class ImportInvoiceDto
{
    [JsonProperty("Number")]
    [Required]
    [Range(ValidationConstants.MIN_INVOICE_NUMBER_NAME_LENGTH, ValidationConstants.MAX_INVOICE_NUMBER_NAME_LENGTH)]
    public int Number { get; set; }

    [JsonProperty("IssueDate")]
    [Required]
    public string IssueDate { get; set; } = null!;

    [JsonProperty("DueDate")]
    [Required]
    public string DueDate { get; set; } = null!;

    [JsonProperty("Amount")]
    [Required]
    public decimal Amount { get; set; }

    [JsonProperty("CurrencyType")]
    [Required]
    [Range(ValidationConstants.MIN_CURRENCY_TYPE_VALUE, ValidationConstants.MAX_CURRENCY_TYPE_VALUE)]
    public CurrencyType CurrencyType { get; set; }

    [JsonProperty("ClientId")]
    [Required]
    public int ClientId { get; set; }
}