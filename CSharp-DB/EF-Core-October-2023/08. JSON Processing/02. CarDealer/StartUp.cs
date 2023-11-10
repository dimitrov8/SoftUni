namespace CarDealer;

using AutoMapper;
using Data;
using DTOs.Import;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
        string result = ImportCars(context, File.ReadAllText(@"../../../Datasets/cars.json"));
        Console.WriteLine(result);
    }

    // 09. Import Suppliers 
    public static string ImportSuppliers(CarDealerContext context, string inputJson)
    {
        var mapper = CreateMapper();

        ImportSupplierDto[] supplierDtos = JsonConvert.DeserializeObject<ImportSupplierDto[]>(inputJson);

        Supplier[] suppliers = mapper.Map<Supplier[]>(supplierDtos);

        context.Suppliers.AddRange(suppliers);
        context.SaveChanges();

        return $"Successfully imported {suppliers.Length}.";
    }

    // 10. Import Parts 
    public static string ImportParts(CarDealerContext context, string inputJson)
    {
        var mapper = CreateMapper();

        int[] supplierIds = context.Suppliers
            .Select(s => s.Id)
            .ToArray();

        ImportPartDto[] importPartDtos = JsonConvert.DeserializeObject<ImportPartDto[]>(inputJson);

        Part[] parts = mapper.Map<Part[]>(importPartDtos)
            .Where(p => supplierIds.Contains(p.SupplierId))
            .ToArray();


        context.Parts.AddRange(parts);
        context.SaveChanges();

        return $"Successfully imported {parts.Length}.";
    }

    // 11. Import Cars 
    public static string ImportCars(CarDealerContext context, string inputJson)
    {
        var mapper = CreateMapper();

        ImportCarDto[] importCarDtos = JsonConvert.DeserializeObject<ImportCarDto[]>(inputJson);

        Car[] cars = mapper.Map<Car[]>(importCarDtos);

        context.Cars.AddRange(cars);

        foreach (var car in cars)
        {
            context.PartsCars.AddRange(car.PartsCars);
        }

        context.SaveChanges();

        return $"Successfully imported {cars.Length}.";
    }

    public static IMapper CreateMapper() => new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<CarDealerProfile>(); }));

    public static IContractResolver ConfigureCamelCaseNaming()
        => new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy(false, true)
        };
}