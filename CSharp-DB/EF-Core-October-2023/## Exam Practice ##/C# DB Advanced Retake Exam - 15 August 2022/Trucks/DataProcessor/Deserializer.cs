namespace Trucks.DataProcessor;

using Data;
using Data.Models;
using Data.Models.Enums;
using ImportDto;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Utilities;

public class Deserializer
{
    private const string ERROR_MESSAGE = "Invalid data!";

    private const string SUCCESSFULLY_IMPORTED_DESPATCHER
        = "Successfully imported despatcher - {0} with {1} trucks.";

    private const string SUCCESSFULLY_IMPORTED_CLIENT
        = "Successfully imported client - {0} with {1} trucks.";

    public static string ImportDespatcher(TrucksContext context, string xmlString)
    {
        var xmlHelper = new XmlHelper();
        var sb = new StringBuilder();

        ImportDespatcherDto[] despatcherDtos = xmlHelper.Deserialize<ImportDespatcherDto[]>(xmlString, "Despatchers");

        var validDespatchers = new HashSet<Despatcher>();

        foreach (var despatcherDto in despatcherDtos)
        {
            if (!IsValid(despatcherDto))
            {
                sb.AppendLine(ERROR_MESSAGE);
                continue;
            }

            var validTrucks = new HashSet<Truck>();

            foreach (var truckDto in despatcherDto.Trucks)
            {
                if (!IsValid(truckDto))
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var truck = new Truck
                {
                    RegistrationNumber = truckDto.RegistrationNumber,
                    VinNumber = truckDto.VinNumber,
                    TankCapacity = truckDto.TankCapacity,
                    CargoCapacity = truckDto.CargoCapacity,
                    CategoryType = (CategoryType)truckDto.CategoryType,
                    MakeType = (MakeType)truckDto.MakeType
                };

                validTrucks.Add(truck);
            }

            var despatcher = new Despatcher
            {
                Name = despatcherDto.Name,
                Position = despatcherDto.Position,
                Trucks = validTrucks
            };

            validDespatchers.Add(despatcher);
            sb.AppendLine(string.Format(SUCCESSFULLY_IMPORTED_DESPATCHER, despatcher.Name, validTrucks.Count));
        }

        context.Despatchers.AddRange(validDespatchers);
        context.SaveChanges();

        return sb.ToString().TrimEnd();
    }

    public static string ImportClient(TrucksContext context, string jsonString)
    {
        int[] existrinTruckIds = context.Trucks
            .Select(t => t.Id)
            .ToArray();

        ImportClientDto[]? clientDtos = JsonConvert.DeserializeObject<ImportClientDto[]>(jsonString);
        var sb = new StringBuilder();

        var validClients = new HashSet<Client>();
        foreach (var clientDto in clientDtos)
        {
            if (!IsValid(clientDto) ||
                clientDto.Type == "usual")
            {
                sb.AppendLine(ERROR_MESSAGE);
                continue;
            }

            var client = new Client
            {
                Name = clientDto.Name,
                Nationality = clientDto.Nationality,
                Type = clientDto.Type
            };

            foreach (int truckId in clientDto.TruckIds.Distinct())
            {
                if (!existrinTruckIds.Contains(truckId))
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var clientTruck = new ClientTruck
                {
                    Client = client,
                    TruckId = truckId
                };

                client.ClientsTrucks.Add(clientTruck);
            }

            validClients.Add(client);
            sb.AppendLine(string.Format(SUCCESSFULLY_IMPORTED_CLIENT, client.Name, client.ClientsTrucks.Count));
        }

        context.Clients.AddRange(validClients);
        context.SaveChanges();

        return sb.ToString().TrimEnd();
    }

    private static bool IsValid(object dto)
    {
        var validationContext = new ValidationContext(dto);
        var validationResult = new List<ValidationResult>();

        return Validator.TryValidateObject(dto, validationContext, validationResult, true);
    }
}