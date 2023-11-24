namespace Boardgames.DataProcessor.ImportDto;

using Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

public class ImportSellerDto
{
    [Required]
    [JsonProperty("Name")]
    [MinLength(ValidationConstants.MIN_SELLER_NAME_LENGTH)]
    [MaxLength(ValidationConstants.MAX_SELLER_NAME_LENGTH)]
    public string Name { get; set; } = null!;

    [Required]
    [JsonProperty("Address")]
    [MinLength(ValidationConstants.MIN_SELLER_ADDRESS_LENGTH)]
    [MaxLength(ValidationConstants.MAX_SELLER_ADDRESS_LENGTH)]
    public string Address { get; set; } = null!;

    [Required]
    [JsonProperty("Country")]
    public string Country { get; set; } = null!;

    [Required]
    [JsonProperty("Website")]
    [RegularExpression(ValidationConstants.REGEX_SELLER_WEBSITE)]
    public string Website { get; set; } = null!;

    [JsonProperty("Boardgames")]
    public int[] Boardgames { get; set; } = null!;
}