namespace CarDealer;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using DTOs.Export;
using DTOs.Import;
using Microsoft.EntityFrameworkCore;
using Models;
using Utilities;

public class StartUp
{
    public static void Main()
    {
        using var context = new CarDealerContext();

        // Import
        //string inputXml = File.ReadAllText("../../../Datasets/sales.xml");
        //string result = ImportSales(context, inputXml);
        //Console.WriteLine(result);


        // Export
        string result = GetSalesWithAppliedDiscount(context);
        Console.WriteLine(result);
    }

    // 09. Import Suppliers
    public static string ImportSuppliers(CarDealerContext context, string inputXml)
    {
        var mapper = InitializeMapper();
        var xmlHelper = new XmlHelper();

        ImportSupplierDto[] supplierDtos = xmlHelper.Deserialize<ImportSupplierDto[]>(inputXml, "Suppliers");

        ICollection<Supplier> validSuppliers = new HashSet<Supplier>();

        foreach (var supplierDto in supplierDtos)
        {
            if (string.IsNullOrEmpty(supplierDto.Name))
            {
                continue;
            }

            var supplier = mapper.Map<Supplier>(supplierDto);
            validSuppliers.Add(supplier);
        }

        context.Suppliers.AddRange(validSuppliers);
        context.SaveChanges();

        return $"Successfully imported {validSuppliers.Count}";
    }

    // 10. Import Parts 
    public static string ImportParts(CarDealerContext context, string inputXml)
    {
        var xmlHelper = new XmlHelper();

        ImportPartDto[] partDtos = xmlHelper.Deserialize<ImportPartDto[]>(inputXml, "Parts");
        int[] existingSupplierIds = context.Suppliers.Select(s => s.Id).ToArray();

        ICollection<Part> validParts = new HashSet<Part>();

        foreach (var partDto in partDtos)
        {
            int partSupplierId = partDto.SupplierId;

            if (!existingSupplierIds.Contains(partSupplierId))
            {
                continue;
            }

            var part = new Part
            {
                Name = partDto.Name,
                Price = partDto.Price,
                Quantity = partDto.Quantity,
                SupplierId = partDto.SupplierId
            };

            validParts.Add(part);
        }

        context.Parts.AddRange(validParts);
        context.SaveChanges();

        return $"Successfully imported {validParts.Count}";
    }

    // 11. Import Cars 
    public static string ImportCars(CarDealerContext context, string inputXml)
    {
        var xmlHelper = new XmlHelper();

        ImportCarDto[] carDtos = xmlHelper.Deserialize<ImportCarDto[]>(inputXml, "Cars");
        int[] existingCarPartIds = context.Parts
            .Select(p => p.Id)
            .ToArray(); // Existing parts in the context

        ICollection<Car> validCars = new HashSet<Car>(); // Holds all valid cars 

        foreach (var carDto in carDtos)
        {
            if (string.IsNullOrEmpty(carDto.Make)
                || string.IsNullOrEmpty(carDto.Model))
            {
                continue;
            }

            var car = new Car
            {
                Make = carDto.Make,
                Model = carDto.Model,
                TraveledDistance = carDto.TraveledDistance
            };

            ICollection<int> uniqueCarPartIds = carDto.Parts
                .Select(c => c.PartId)
                .Where(partId => existingCarPartIds.Contains(partId))
                .Distinct()
                .ToArray(); // Take only the partIds that are unique and are available in the context 

            foreach (int carPartId in uniqueCarPartIds) // For each carPartId
            {
                var carPart = new PartCar
                {
                    PartId = carPartId,
                    Car = car
                };

                context.PartsCars.Add(carPart); // Add the PartCar to the context
            }

            validCars.Add(car); // Add the car in the valid cars hashset
        }

        context.Cars.AddRange(validCars);
        context.SaveChanges();

        return $"Successfully imported {validCars.Count}";
    }

    // 12. Import Customers 
    public static string ImportCustomers(CarDealerContext context, string inputXml)
    {
        var xmlHelper = new XmlHelper();

        ImportCustomerDto[] customerDtos = xmlHelper.Deserialize<ImportCustomerDto[]>(inputXml, "Customers");

        ICollection<Customer> validCustomers = new HashSet<Customer>();

        foreach (var customerDto in customerDtos)
        {
            if (string.IsNullOrEmpty(customerDto.Name)
                || string.IsNullOrEmpty(customerDto.BirthDate))
            {
                continue;
            }

            var customer = new Customer
            {
                Name = customerDto.Name,
                BirthDate = DateTime.Parse(customerDto.BirthDate),
                IsYoungDriver = customerDto.IsYoungDriver
            };

            validCustomers.Add(customer);
        }

        context.Customers.AddRange(validCustomers);
        context.SaveChanges();

        return $"Successfully imported {validCustomers.Count}";
    }

    // 13. Import Sales 
    public static string ImportSales(CarDealerContext context, string inputXml)
    {
        var mapper = InitializeMapper();
        var xmlHelper = new XmlHelper();

        ImportSaleDto[] saleDtos = xmlHelper.Deserialize<ImportSaleDto[]>(inputXml, "Sales");

        int[] existingCarIds = context.Cars
            .Select(c => c.Id)
            .ToArray(); // Takes all car id's in the context

        Sale[] sales = mapper.Map<Sale[]>(saleDtos)
            .Where(s => existingCarIds.Contains(s.CarId))
            .ToArray(); // Maps only if the sale contains a car that exists in the context

        context.Sales.AddRange(sales);
        context.SaveChanges();

        return $"Successfully imported {sales.Length}";
    }

    // 14. Export Cars With Distance
    public static string GetCarsWithDistance(CarDealerContext context)
    {
        var mapper = InitializeMapper();
        var xmlHelper = new XmlHelper();

        ExportCarDto[] cars = context.Cars
            .Where(c => c.TraveledDistance > 2000000)
            .OrderBy(c => c.Make)
            .ThenBy(c => c.Model)
            .Take(10)
            .ProjectTo<ExportCarDto>(mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToArray();

        return xmlHelper.Serialize<ExportCarDto[]>(cars, "cars");
    }

    // 15. Export Cars from Make BMW
    public static string GetCarsFromMakeBmw(CarDealerContext context)
    {
        var mapper = InitializeMapper();
        var xmlHelper = new XmlHelper();

        ExportCarMakeBmwDto[] cars = context.Cars
            .Where(c => c.Make == "BMW")
            .ProjectTo<ExportCarMakeBmwDto>(mapper.ConfigurationProvider)
            .OrderBy(c => c.Model)
            .ThenByDescending(c => c.TraveledDistance)
            .AsNoTracking()
            .ToArray();

        return xmlHelper.Serialize(cars, "cars");
    }

    // 16. Export Local Suppliers 
    public static string GetLocalSuppliers(CarDealerContext context)
    {
        var mapper = InitializeMapper();
        var xmlHelper = new XmlHelper();

        ExportLocalSupplierDto[] suppliers = context.Suppliers
            .Where(s => !s.IsImporter)
            .ProjectTo<ExportLocalSupplierDto>(mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToArray();

        return xmlHelper.Serialize<ExportLocalSupplierDto[]>(suppliers, "suppliers");
    }

    // 17. Export Cars With Their List Of Parts 
    public static string GetCarsWithTheirListOfParts(CarDealerContext context)
    {
        var xmlHelper = new XmlHelper();

        ExportCarAndPartsDto[] carsAndParts = context.Cars
            .Select(c => new ExportCarAndPartsDto
            {
                Make = c.Make,
                Model = c.Model,
                TraveledDistance = c.TraveledDistance,
                Parts = c.PartsCars
                    .Select(pc => new ExportPartDto
                    {
                        Name = pc.Part.Name,
                        Price = pc.Part.Price
                    })
                    .OrderByDescending(pc => pc.Price)
                    .ToArray()
            })
            .AsNoTracking()
            .OrderByDescending(c => c.TraveledDistance)
            .ThenBy(c => c.Model)
            .Take(5)
            .ToArray();

        return xmlHelper.Serialize<ExportCarAndPartsDto[]>(carsAndParts, "cars");
    }

    // 18. Export Total Sales by Customer
    public static string GetTotalSalesByCustomer(CarDealerContext context)
    {
        var xmlHelper = new XmlHelper();

        Customer[] customersWithSales = context.Customers
            .Include(c => c.Sales)
            .Where(c => c.Sales.Any())
            .ToArray();

        ExportCustomerTotalSalesDto[] customers = customersWithSales
            .Select(c => new ExportCustomerTotalSalesDto
            {
                Name = c.Name,
                BoughtCars = c.Sales.Count,
                SpentMoney = c.Sales
                    .SelectMany(s => s.Car.PartsCars)
                    .Select(pc => c.IsYoungDriver
                        ? (decimal)Math.Round((double)pc.Part.Price * 0.95, 2)
                        : pc.Part.Price)
                    .Sum()
            })
            .OrderByDescending(c => c.SpentMoney)
            .ToArray();

        return xmlHelper.Serialize<ExportCustomerTotalSalesDto[]>(customers, "customers");
    }

    // 19. Export Sales With Applied Discount 
    public static string GetSalesWithAppliedDiscount(CarDealerContext context)
    {
        var xmlHelper = new XmlHelper();

        ExportSaleWithDiscountDto[] salesWithDiscounts = context.Sales
            .Select(s => new ExportSaleWithDiscountDto
            {
                Car = new ExportCarAttributeDto
                {
                    Make = s.Car.Make,
                    Model = s.Car.Model,
                    TraveledDistance = s.Car.TraveledDistance
                },
                Discount = s.Discount,
                CustomerName = s.Customer.Name,
                Price = s.Car.PartsCars.Sum(pc => pc.Part.Price)
            })
            .AsNoTracking()
            .ToArray();

        return xmlHelper.Serialize<ExportSaleWithDiscountDto[]>(salesWithDiscounts, "sales");
    }

    public static IMapper InitializeMapper()
        => new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<CarDealerProfile>(); }));
}