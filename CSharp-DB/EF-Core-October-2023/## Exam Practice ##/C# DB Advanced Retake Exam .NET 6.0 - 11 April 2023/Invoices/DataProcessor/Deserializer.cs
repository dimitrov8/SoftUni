namespace Invoices.DataProcessor;

using Data;
using Data.Models;
using ImportDto;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Utility;

public class Deserializer
{
    private const string ERROR_MESSAGE = "Invalid data!";

    private const string SUCCESSFULLY_IMPORTED_CLIENTS
        = "Successfully imported client {0}.";

    private const string SUCCESSFULLY_IMPORTED_INVOICES
        = "Successfully imported invoice with number {0}.";

    private const string SUCCESSFULLY_IMPORTED_PRODUCTS
        = "Successfully imported product - {0} with {1} clients.";

    private static XmlHelper xmlHelper;

    public static string ImportClients(InvoicesContext context, string xmlString)
    {
        var sb = new StringBuilder();
        xmlHelper = new XmlHelper();

        ImportClientDto[] clientDtos = xmlHelper.Deserialize<ImportClientDto[]>(xmlString, "Clients");

        ICollection<Client> validClients = new HashSet<Client>();

        foreach (var clientDto in clientDtos)
        {
            if (!IsValid(clientDto))
            {
                sb.AppendLine(ERROR_MESSAGE);
                continue;
            }

            ICollection<Address> validAddresses = new HashSet<Address>();

            foreach (var addressDto in clientDto.Addresses)
            {
                if (!IsValid(addressDto))
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var address = new Address
                {
                    StreetName = addressDto.StreetName,
                    StreetNumber = addressDto.StreetNumber,
                    PostCode = addressDto.PostCode,
                    City = addressDto.City,
                    Country = addressDto.Country
                };

                validAddresses.Add(address);
            }

            var client = new Client
            {
                Name = clientDto.Name,
                NumberVat = clientDto.NumberVat,
                Addresses = validAddresses
            };

            validClients.Add(client);
            sb.AppendLine(string.Format(SUCCESSFULLY_IMPORTED_CLIENTS, client.Name));
        }
        context.Clients.AddRange(validClients);
        context.SaveChanges();

        return sb.ToString().TrimEnd();
    }


    public static string ImportInvoices(InvoicesContext context, string jsonString)
    {
        throw new NotImplementedException();
    }

    public static string ImportProducts(InvoicesContext context, string jsonString)
    {
        throw new NotImplementedException();
    }

    public static bool IsValid(object dto)
    {
        var validationContext = new ValidationContext(dto);
        var validationResult = new List<ValidationResult>();

        return Validator.TryValidateObject(dto, validationContext, validationResult, true);
    }
}