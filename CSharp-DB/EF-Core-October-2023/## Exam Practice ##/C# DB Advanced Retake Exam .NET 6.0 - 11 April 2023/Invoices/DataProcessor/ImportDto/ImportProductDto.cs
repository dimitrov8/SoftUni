namespace Invoices.DataProcessor.ImportDto;

using Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

public class ImportProductDto
{
    [Required]
    [StringLength(ValidationConstants.MAX_PRODUCT_NAME_LENGTH, MinimumLength = ValidationConstants.MIN_PRODUCT_NAME_LENGTH)]
    public string Name { get; set; } = null!;

    [Required]
    [Range(ValidationConstants.MIN_PRODUCT_PRICE, ValidationConstants.MAX_PRODUCT_PRICE)]
    public decimal Price { get; set; }

    [Required]
    [Range(ValidationConstants.MIN_CATEGORY_TYPE_VALUE, ValidationConstants.MAX_CATEGORY_TYPE_VALUE)]
    public string CategoryType { get; set; } = null!;

    [JsonProperty("Clients")]
    public int[] ClientIds { get; set; } = null!;
}