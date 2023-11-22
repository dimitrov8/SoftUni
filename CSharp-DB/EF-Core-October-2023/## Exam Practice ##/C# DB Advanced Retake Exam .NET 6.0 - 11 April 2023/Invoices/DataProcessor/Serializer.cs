namespace Invoices.DataProcessor;

using Data;
using ExportDto;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using Utility;

public class Serializer
{
    public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
    {
        var xmlHelper = new XmlHelper();

        ExportClientDto[] clients = context.Clients
            .Include(c => c.Invoices)
            .Where(c => c.Invoices.Any(i => i.IssueDate > date))
            .AsNoTracking()
            .OrderByDescending(c => c.Invoices.Count)
            .ThenBy(c => c.Name)
            .ToArray()
            .Select(c => new ExportClientDto
            {
                ClientName = c.Name,
                InvoicesCount = c.Invoices.Count,
                VatNumber = c.NumberVat,
                Invoices = c.Invoices.Select(i => new ExportInvoiceDto
                    {
                        InvoiceNumber = i.Number,
                        InvoiceAmount = decimal.Parse(i.Amount.ToString("0.##")),
                        IssueDate = i.IssueDate.ToString("d", CultureInfo.InvariantCulture),
                        DueDate = i.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        Currency = i.CurrencyType.ToString()
                    })
                    .OrderBy(i => DateTime.ParseExact(i.IssueDate, "d", CultureInfo.InvariantCulture))
                    .ThenByDescending(i => DateTime.ParseExact(i.DueDate, "d", CultureInfo.InvariantCulture))
                    .ToArray()
            })
            .ToArray();

        return xmlHelper.Serialize(clients, "Clients");
    }

    public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
    {
        var products = context.Products
            .Where(p => p.ProductsClients.Any(pc => pc.Client.Name.Length >= nameLength))
            .Select(p => new
            {
                p.Name,
                Price = decimal.Parse(p.Price.ToString("0.##")),
                Category = p.CategoryType.ToString(),
                Clients = p.ProductsClients
                    .Where(pc => pc.Client.Name.Length >= nameLength)
                    .Select(c => new
                    {
                        c.Client.Name,
                        c.Client.NumberVat
                    })
                    .OrderBy(c => c.Name)
                    .ToArray()
            })
            .AsNoTracking()
            .OrderByDescending(p => p.Clients.Length)
            .ThenBy(p => p.Name)
            .Take(5)
            .ToArray();

        return JsonConvert.SerializeObject(products, Formatting.Indented);
    }
}