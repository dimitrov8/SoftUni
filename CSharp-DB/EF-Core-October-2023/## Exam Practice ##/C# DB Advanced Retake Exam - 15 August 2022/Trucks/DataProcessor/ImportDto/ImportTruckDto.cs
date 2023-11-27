namespace Trucks.DataProcessor.ImportDto;

using Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

[XmlType("Truck")]
public class ImportTruckDto
{
    [XmlElement("RegistrationNumber")]
    [MinLength(ValidationConstants.TRUCK_REGISTRATION_NUMBER_LENGTH)]
    [MaxLength(ValidationConstants.TRUCK_REGISTRATION_NUMBER_LENGTH)]
    [RegularExpression(ValidationConstants.REGEX_STRING_TRUCK_REGISTRATION_NUMBER)]
    public string? RegistrationNumber { get; set; } 

    [XmlElement("VinNumber")]
    [MinLength(ValidationConstants.TRUCK_VIN_NUMBER_LENGTH)]
    [MaxLength(ValidationConstants.TRUCK_VIN_NUMBER_LENGTH)]
    [Required]
    public string VinNumber { get; set; } = null!;

    [XmlElement("TankCapacity")]
    [Range(ValidationConstants.TRUCK_TANK_CAPACITY_MIN, ValidationConstants.TRUCK_TANK_CAPACITY_MAX)]
    public int TankCapacity { get; set; }

    [XmlElement("CargoCapacity")]
    [Range(ValidationConstants.TRUCK_CARGO_CAPACITY_MIN, ValidationConstants.TRUCK_CARGO_CAPACITY_MAX)]
    public int CargoCapacity { get; set; }

    [XmlElement("CategoryType")]
    [Range(ValidationConstants.TRUCK_CATEGORY_TYPE_MIN_VALUE, ValidationConstants.TRUCK_CATEGORY_TYPE_MAX_VALUE)]
    public int CategoryType { get; set; }

    [XmlElement("MakeType")]
    [Range(ValidationConstants.TRUCK_MAKE_TYPE_MIN_VALUE, ValidationConstants.TRUCK_MAKE_TYPE_MAX_VALUE)]
    public int MakeType { get; set; }
}