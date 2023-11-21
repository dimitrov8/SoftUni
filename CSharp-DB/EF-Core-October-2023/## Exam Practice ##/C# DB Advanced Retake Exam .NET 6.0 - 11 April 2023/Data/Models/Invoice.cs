namespace Invoices.Data.Models;

using Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Invoice
{
    [Key]
    public int Id { get; set; }

    [Range(ValidationConstants.MIN_INVOICE_NUMBER_NAME_LENGTH, ValidationConstants.MAX_INVOICE_NUMBER_NAME_LENGTH)]
    public int Number { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime DueDate { get; set; }

    public decimal Amount { get; set; }

    public CurrencyType CurrencyType { get; set; }

    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;
}