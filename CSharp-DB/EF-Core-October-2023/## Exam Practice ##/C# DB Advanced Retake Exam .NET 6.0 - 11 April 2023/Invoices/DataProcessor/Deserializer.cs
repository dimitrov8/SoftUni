namespace Invoices.DataProcessor;

using Data;
using Data.Models;
using Data.Models.Enums;
using ImportDto;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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
        var sb = new StringBuilder();

        ImportInvoiceDto[] invoiceDtos = JsonConvert.DeserializeObject<ImportInvoiceDto[]>(jsonString);
        int[] existingClientIds = context.Clients.Select(c => c.Id).ToArray();

        ICollection<Invoice> validInvoices = new HashSet<Invoice>();

        foreach (var invoiceDto in invoiceDtos)
        {
            if (!IsValid(invoiceDto))
            {
                sb.AppendLine(ERROR_MESSAGE);
                continue;
            }

            var issueDateParse = DateTime.Parse(invoiceDto.IssueDate, CultureInfo.InvariantCulture);
            var dueDateParse = DateTime.Parse(invoiceDto.DueDate, CultureInfo.InvariantCulture);

            if (dueDateParse < issueDateParse ||
                !existingClientIds.Contains(invoiceDto.ClientId))
            {
                sb.AppendLine(ERROR_MESSAGE);
                continue;
            }

            var invoice = new Invoice
            {
                Number = invoiceDto.Number,
                IssueDate = issueDateParse,
                DueDate = dueDateParse,
                Amount = invoiceDto.Amount,
                CurrencyType = invoiceDto.CurrencyType,
                ClientId = invoiceDto.ClientId
            };

            validInvoices.Add(invoice);

            sb.AppendLine(string.Format(SUCCESSFULLY_IMPORTED_INVOICES, invoice.Number));
        }

        context.Invoices.AddRange(validInvoices);
        context.SaveChanges();

        return sb.ToString().TrimEnd();
    }

    public static string ImportProducts(InvoicesContext context, string jsonString)
    {
        var sb = new StringBuilder();

        ImportProductDto[] productDtos = JsonConvert.DeserializeObject<ImportProductDto[]>(jsonString);
        int[] existingClientIds = context.Clients.Select(c => c.Id).ToArray();

        ICollection<Product> products = new HashSet<Product>();

        ICollection<Product> validProducts = new HashSet<Product>();

        foreach (var productDto in productDtos)
        {
            if (!IsValid(productDto))
            {
                sb.AppendLine(ERROR_MESSAGE);
                continue;
            }

            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                CategoryType = (CategoryType)Enum.Parse(typeof(CategoryType), productDto.CategoryType)
            };

            foreach (int clientId in productDto
                         .ClientIds
                         .Distinct())
            {
                if (!existingClientIds.Contains(clientId))
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var productClient = new ProductClient
                {
                    Product = product,
                    ClientId = clientId
                };

                product.ProductsClients.Add(productClient);
            }

            validProducts.Add(product);
            sb.AppendLine(string.Format(SUCCESSFULLY_IMPORTED_PRODUCTS, product.Name, product.ProductsClients.Count));
        }

        context.Products.AddRange(validProducts);
        context.SaveChanges();

        return sb.ToString().TrimEnd();
    }

    public static bool IsValid(object dto)
    {
        var validationContext = new ValidationContext(dto);
        var validationResult = new List<ValidationResult>();

        return Validator.TryValidateObject(dto, validationContext, validationResult, true);
    }
}