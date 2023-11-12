namespace CarDealer;

using AutoMapper;
using Data;
using DTOs.Import;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Globalization;

public class StartUp
{
    public static void Main()
    {
        var context = new CarDealerContext();

        // 09. Import Suppliers 
        //string result = ImportSuppliers(context, File.ReadAllText(@"../../../Datasets/suppliers.json"));
        //Console.WriteLine(result);

        // 10. Import Parts 
        //string result = ImportParts(context, File.ReadAllText(@"../../../Datasets/parts.json"));
        //Console.WriteLine(result);

        // 11. Import Cars 
        //string result = ImportCars(context, File.ReadAllText(@"../../../Datasets/cars.json"));
        //Console.WriteLine(result);

        // 12. Import Customers
        //string result = ImportCustomers(context, File.ReadAllText(@"../../../Datasets/customers.json"));
        //Console.WriteLine(result);

        // 13. Import Sales 
        //string result = ImportSales(context, File.ReadAllText(@"../../../Datasets/sales.json"));
        //Console.WriteLine(result);

        // 14. Export Ordered Customers 
        //string result = GetOrderedCustomers(context);
        //Console.WriteLine(result);

        // 15.Export Cars From Make Toyota
        //string result = GetCarsFromMakeToyota(context);
        //Console.WriteLine(result);

        // 16. Export Local Suppliers
        //string result = GetLocalSuppliers(context);
        //Console.WriteLine(result);

        // 17. Export Cars with Their List of Parts
        //string result = GetCarsWithTheirListOfParts(context);
        //Console.WriteLine(result);

        // 18.Export Total Sales By Customer
        //string result = GetTotalSalesByCustomer(context);
        //Console.WriteLine(result);

        // 19. Export Sales With Applied Discount 
        //string result = GetSalesWithAppliedDiscount(context);
        //Console.WriteLine(result);
    }

    // 09. Import Suppliers 
    public static string ImportSuppliers(CarDealerContext context, string inputJson)
    {
        ImportSupplierDto[] supplierDtos = JsonConvert.DeserializeObject<ImportSupplierDto[]>(inputJson);

        ICollection<Supplier> validSuppliers = new HashSet<Supplier>();

        if (supplierDtos != null)
            foreach (var supplierDto in supplierDtos)
            {
                var supplier = new Supplier
                {
                    IsImporter = supplierDto.IsImporter,
                    Name = supplierDto.Name
                };

                validSuppliers.Add(supplier);
            }

        context.Suppliers.AddRange(validSuppliers);
        context.SaveChanges();

        return $"Successfully imported {validSuppliers.Count}.";
    }

    // 10. Import Parts 
    public static string ImportParts(CarDealerContext context, string inputJson)
    {
        int[] supplierIds = context.Suppliers
            .Select(s => s.Id)
            .ToArray();

        ImportPartDto[] importPartDtos = JsonConvert.DeserializeObject<ImportPartDto[]>(inputJson);

        ICollection<Part> validParts = new HashSet<Part>();

        if (importPartDtos != null)
        {
            foreach (var importPartDto in importPartDtos
                         .Where(dto => supplierIds.Contains(dto.SupplierId)))
            {
                var part = new Part
                {
                    Name = importPartDto.Name,
                    Price = importPartDto.Price,
                    Quantity = importPartDto.Quantity,
                    SupplierId = importPartDto.SupplierId
                };

                part.Supplier = context.Suppliers.Find(part.SupplierId);
                validParts.Add(part);
            }

            context.Parts.AddRange(validParts);

            context.SaveChanges();
        }

        return $"Successfully imported {validParts.Count}.";
    }

    // 11. Import Cars 
    public static string ImportCars(CarDealerContext context, string inputJson)
    {
        var mapper = CreateMapper();

        ImportCarDto[] importCarDtos = JsonConvert.DeserializeObject<ImportCarDto[]>(inputJson);

        ICollection<Car> validCars = new HashSet<Car>();

        if (importCarDtos != null)
            foreach (var carDto in importCarDtos) // For each importCarDto
            {
                var car = mapper.Map<Car>(carDto); // Map the ImportCarDto to a Car entity

                int[] existingPartsIds = context.Parts
                    .Where(p => carDto.PartsId.Contains(p.Id))
                    .Select(p => p.Id)
                    .ToArray(); // Take the existing parts ids

                PartCar[] carParts = existingPartsIds.Select(partId => new PartCar
                {
                    PartId = partId
                }).ToArray(); // Create an array of PartCar instances using the existing part IDs

                car.PartsCars = carParts; // Assign the array of PartCar instances to the car's PartsCars collection

                validCars.Add(car); // Add the car to the validCars collection
            }

        context.Cars.AddRange(validCars); // Add all valid cars to the context's Cars collection
        //  context.SaveChanges(); // Save changes to the database

        return $"Successfully imported {validCars.Count}.";
    }

    // 12. Import Customers
    public static string ImportCustomers(CarDealerContext context, string inputJson)
    {
        var mapper = CreateMapper();

        ImportCustomerDto[] customersDtos = JsonConvert.DeserializeObject<ImportCustomerDto[]>(inputJson);

        ICollection<Customer> validCustomers = new HashSet<Customer>();

        if (customersDtos != null)
        {
            foreach (var customerDto in customersDtos)
            {
                var customer = mapper.Map<Customer>(customerDto);

                validCustomers.Add(customer);
            }

            context.Customers.AddRange(validCustomers);
        }

        context.SaveChanges();
        return $"Successfully imported {validCustomers.Count}.";
    }

    // 13. Import Sales 
    public static string ImportSales(CarDealerContext context, string inputJson)
    {
        var mapper = CreateMapper();

        ImportSaleDto[] saleDtos = JsonConvert.DeserializeObject<ImportSaleDto[]>(inputJson);

        ICollection<Sale> validSales = new HashSet<Sale>();

        if (saleDtos != null)
        {
            foreach (var saleDto in saleDtos)
            {
                var sale = mapper.Map<Sale>(saleDto);

                // Set navigation properties
                sale.Car = context.Cars.Find(sale.CarId);
                sale.Customer = context.Customers.Find(saleDto.CustomerId);

                validSales.Add(sale);
            }
        }

        context.Sales.AddRange(validSales);

        context.SaveChanges();

        return $"Successfully imported {validSales.Count}.";
    }

    // 14. Export Ordered Customers 
    public static string GetOrderedCustomers(CarDealerContext context)
    {
        var customer = context.Customers
            .OrderBy(c => c.BirthDate)
            // If two customers are born on the same date first print those who are not young drivers
            .ThenBy(c => c.IsYoungDriver ? 1 : 0)
            .Select(c => new
            {
                c.Name,
                BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                c.IsYoungDriver
            })
            .AsNoTracking()
            .ToArray();

        return JsonConvert.SerializeObject(customer, Formatting.Indented);
    }

    // 15. Export Cars From Make Toyota 
    public static string GetCarsFromMakeToyota(CarDealerContext context)
    {
        var cars = context.Cars
            .Where(c => c.Make == "Toyota")
            .Select(c => new
            {
                c.Id,
                c.Make,
                c.Model,
                c.TraveledDistance
            })
            .AsNoTracking()
            .OrderBy(c => c.Model)
            .ThenByDescending(c => c.TraveledDistance)
            .ToArray();

        return JsonConvert.SerializeObject(cars, Formatting.Indented);
    }

    // 16. Export Local Suppliers
    public static string GetLocalSuppliers(CarDealerContext context)
    {
        var suppliers = context.Suppliers
            .Where(s => !s.IsImporter)
            .Select(s => new
            {
                s.Id,
                s.Name,
                PartsCount = s.Parts.Count
            })
            .AsNoTracking()
            .ToArray();

        return JsonConvert.SerializeObject(suppliers, Formatting.Indented);
    }

    // 17. Export Cars with Their List of Parts
    public static string GetCarsWithTheirListOfParts(CarDealerContext context)
    {
        var cars = context.Cars
            .Select(c => new
            {
                car = new
                {
                    c.Make,
                    c.Model,
                    c.TraveledDistance
                },

                parts = c.PartsCars.Select(pc => new
                {
                    pc.Part.Name,
                    Price = pc.Part.Price.ToString("F2")
                })
            })
            .AsNoTracking()
            .ToArray();

        return JsonConvert.SerializeObject(cars, Formatting.Indented);
    }

    // 18. Export Total Sales by Customer
    public static string GetTotalSalesByCustomer(CarDealerContext context)
    {
        var customers = context.Customers
            .Where(c => c.Sales.Count > 0)
            .Select(c => new
            {
                FullName = c.Name,
                BoughtCars = c.Sales.Count,
                SpentMoney = c.Sales.Sum(s => s.Car.PartsCars.Sum(pc => pc.Part.Price))
            })
            .AsNoTracking()
            .OrderByDescending(c => c.SpentMoney)
            .ThenByDescending(c => c.BoughtCars)
            .ToArray();

        var settings = JsonCamelCaseAndFormatIndentedJsonSerializerSettings();

        return JsonConvert.SerializeObject(customers, Formatting.Indented, settings);
    }

    // 19. Export Sales With Applied Discount 
    public static string GetSalesWithAppliedDiscount(CarDealerContext context)
    {
        var sales = context.Sales
            .Take(10)
            .Select(s => new
            {
                car = new
                {
                    s.Car.Make,
                    s.Car.Model,
                    s.Car.TraveledDistance
                },

                customerName = s.Customer.Name,
                discount = s.Discount.ToString("F2"),
                price = s.Car.PartsCars.Sum(pc => pc.Part.Price).ToString("F2"),
                priceWithDiscount = (s.Car.PartsCars.Sum(pc => pc.Part.Price) * (1 - s.Discount / 100)).ToString("F2")
            })
            .AsNoTracking()
            .ToArray();

        return JsonConvert.SerializeObject(sales, Formatting.Indented);
    }

    public static IMapper CreateMapper() => new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<CarDealerProfile>(); }));

    public static IContractResolver ConfigureCamelCaseNaming()
        => new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy(false, true)
        };

    public static JsonSerializerSettings JsonCamelCaseAndFormatIndentedJsonSerializerSettings() => new()
    {
        ContractResolver = ConfigureCamelCaseNaming(),
        Formatting = Formatting.Indented
    };
}