namespace Boardgames.DataProcessor.ExportDto;

using Newtonsoft.Json;

public class ExportSellerDto
{
    [JsonProperty("Name")]
    public string Name { get; set; } = null!;

    [JsonProperty("Website")]
    public string Website { get; set; } = null!;

    [JsonProperty("Boardgames")]
    public ExportBoardgameDto[] Boardgames { get; set; } = null!;
}