namespace ProductShop.DTOs.Export;

using Newtonsoft.Json;

public class ExportSoldProductDto
{
    [JsonProperty("name")]
    public string ProductName { get; set; } = null!;

    [JsonProperty("price")]
    public decimal ProductPrice { get; set; }

    [JsonProperty("buyerFirstName")]
    public string? BuyerFirstName { get; set; }

    [JsonProperty("buyerLastName")]
    public string? BuyerLastName { get; set; }
}