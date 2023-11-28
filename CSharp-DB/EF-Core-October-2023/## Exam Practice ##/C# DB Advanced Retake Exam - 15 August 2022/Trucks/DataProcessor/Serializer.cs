namespace Trucks.DataProcessor;

using Data;
using Data.Models.Enums;
using ExportDto;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Utilities;

public class Serializer
{
    public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
    {
        var xmlHelper = new XmlHelper();

        var despatchers = context.Despatchers
            .Where(d => d.Trucks.Any())
            .Select(d => new ExportDespatcherWIthTruckDto
            {
                TrucksCount = d.Trucks.Count,
                Name = d.Name,
                Trucks = d.Trucks
                    .Select(t => new ExportTruckDto
                    {
                        RegistrationNumber = t.RegistrationNumber,
                        MakeType = t.MakeType.ToString()
                    })
                    .OrderBy(t => t.RegistrationNumber)
                    .ToArray()
            })
            .AsNoTracking()
            .OrderByDescending(d => d.Trucks.Count())
            .ThenBy(d => d.Name)
            .ToArray();

        return xmlHelper.Serialize(despatchers, "Despatchers");
    }

    public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
    {
        var clients = context.Clients
            .Include(c => c.ClientsTrucks)
            .ThenInclude(ct => ct.Truck)
            .AsNoTracking()
            .ToArray()
            .Where(c => c.ClientsTrucks.Any(ct => ct.Truck.TankCapacity >= capacity))
            .Select(c => new
            {
                c.Name,
                Trucks = c.ClientsTrucks
                    .Where(ct => ct.Truck.TankCapacity >= capacity)
                    .Select(ct => new
                    {
                        TruckRegistrationNumber = ct.Truck.RegistrationNumber,
                        ct.Truck.VinNumber,
                        ct.Truck.TankCapacity,
                        ct.Truck.CargoCapacity,
                        CategoryType = ct.Truck.CategoryType.ToString(),
                        MakeType = ct.Truck.MakeType.ToString()
                    })
                    .OrderBy(t => t.MakeType)
                    .ThenByDescending(t => t.CargoCapacity)
                    .ToArray()
            })
            .OrderByDescending(c => c.Trucks.Length)
            .ThenBy(c => c.Name)
            .Take(10)
            .ToArray();

        return JsonConvert.SerializeObject(clients, Formatting.Indented);
    }
}