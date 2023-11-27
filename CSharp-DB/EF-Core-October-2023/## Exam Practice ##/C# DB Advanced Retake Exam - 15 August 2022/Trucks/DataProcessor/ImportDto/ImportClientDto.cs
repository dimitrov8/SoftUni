namespace Trucks.DataProcessor.ImportDto;

using Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

public class ImportClientDto
{
    [JsonProperty("Name")]
    [MinLength(ValidationConstants.CLIENT_NAME_MIN_LENGTH)]
    [MaxLength(ValidationConstants.CLIENT_NAME_MAX_LENGTH)]
    [Required]
    public string Name { get; set; } = null!;

    [JsonProperty("Nationality")]
    [MinLength(ValidationConstants.CLIENT_NATIONALITY_MIN_LENGTH)]
    [MaxLength(ValidationConstants.CLIENT_NATIONALITY_MAX_LENGTH)]
    [Required]
    public string Nationality { get; set; } = null!;

    [JsonProperty("Type")]
    [Required]
    public string Type { get; set; } = null!;

    [JsonProperty("Trucks")]
    public int[] TruckIds { get; set; }
}