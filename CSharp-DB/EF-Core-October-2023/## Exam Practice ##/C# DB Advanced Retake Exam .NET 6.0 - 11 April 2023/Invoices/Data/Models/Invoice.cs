namespace Invoices.Data.Models;

using Enums;
using Invoices.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Invoice
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Range(ValidationConstants.MIN_INVOICE_NUMBER_NAME_LENGTH, ValidationConstants.MAX_INVOICE_NUMBER_NAME_LENGTH)]
    public int Number { get; set; }

    [Required]
    public DateTime IssueDate { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public CurrencyType CurrencyType { get; set; }

    [Required]
    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;
}