namespace ProductShop.DTOs.Export;

using Newtonsoft.Json;

public class ExportUserDto
{
    [JsonProperty(Order = 1, PropertyName = "firstName")]
    public string? FirstName { get; set; }

    [JsonProperty(Order = 2, PropertyName = "lastName")]
    public string LastName { get; set; } = null!;

    [JsonProperty(Order = 3, PropertyName = "soldProducts")]
    public ICollection<ExportSoldProductDto> SoldProducts = new HashSet<ExportSoldProductDto>();
}