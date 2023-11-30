namespace Footballers.DataProcessor.ImportDto;

using Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

public class ImportTeamDto
{
    [JsonProperty("Name")]
    [Required]
    [RegularExpression(ValidationConstants.TEAM_NAME_REGEX)]
    public string Name { get; set; } = null!;

    [JsonProperty("Nationality")]
    [Required]
    [MinLength(ValidationConstants.TEAM_NATIONALITY_MIN_LENGTH)]
    [MaxLength(ValidationConstants.TEAM_NATIONALITY_MAX_LENGTH)]
    public string Nationality { get; set; } = null!;

    [JsonProperty("Trophies")]
    [Required]
    public int Trophies { get; set; }

    [JsonProperty("Footballers")]
    public int[] Footballers { get; set; } = null!;
}