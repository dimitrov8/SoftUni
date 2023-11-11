namespace CarDealer.DTOs.Export;

public class ExportOrderedCustomerDto
{
    public string Name { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public bool IsYoungDriver { get; set; }
}