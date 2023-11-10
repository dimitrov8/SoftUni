namespace ProductShop;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using DTOs.Export;
using DTOs.Import;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

public class StartUp
{
    public static void Main()
    {
        var context = new ProductShopContext();

        // 01. Import Users 
        //string inputJson = File.ReadAllText(@"../../../Datasets/users.json");
        //string result = ImportUsers(context, inputJson);
        //Console.WriteLine(result);

        // 02. Import Products 
        //string inputJson = File.ReadAllText(@"../../../Datasets/products.json");
        //string result = ImportProducts(context, inputJson);
        //Console.WriteLine(result);

        // 03. Import Categories 
        //string inputJson = File.ReadAllText(@"../../../Datasets/categories.json");
        //string result = ImportCategories(context, inputJson);
        //Console.WriteLine(result);

        // 04. Import Categories and Products
        //string inputJson = File.ReadAllText(@"../../../Datasets/categories-products.json");
        //string result = ImportCategoryProducts(context, inputJson);
        //Console.WriteLine(result);

        // 05.Export Products In Range
        //string result = GetProductsInRange(context);
        //Console.WriteLine(result);

        // 06. Export Sold Products 
        //string result = GetSoldProducts(context);
        //Console.WriteLine(result);

        // TODO =>

        // 07. Export Categories By Products Count 
        //string result = GetCategoriesByProductsCount(context);
        //Console.WriteLine(result);

        // 08. Export Users and Products 
        //string result = GetUsersWithProducts(context);
        //Console.WriteLine(result);
    }

    // 01. Import Users 
    public static string ImportUsers(ProductShopContext context, string inputJson)
    {
        var mapper = CreateMapper();

        ImportUserDto[] userDtos = JsonConvert.DeserializeObject<ImportUserDto[]>(inputJson);

        ICollection<User> validUsers = new HashSet<User>();

        foreach (var userDto in userDtos)
        {
            var user = mapper.Map<User>(userDto);

            validUsers.Add(user);
        }

        context.AddRange(validUsers);
        context.SaveChanges();

        return $"Successfully imported {validUsers.Count}";
    }

    // 02. Import Products 
    public static string ImportProducts(ProductShopContext context, string inputJson)
    {
        var mapper = CreateMapper();

        ImportProductDto[] productDtos = JsonConvert.DeserializeObject<ImportProductDto[]>(inputJson);

        Product[] products = mapper.Map<Product[]>(productDtos);

        context.Products.AddRange(products);
        context.SaveChanges();

        return $"Successfully imported {products.Length}";
    }

    // 03. Import Categories 
    public static string ImportCategories(ProductShopContext context, string inputJson)
    {
        var mapper = CreateMapper();

        ImportCategoryDto[] categoryDtos = JsonConvert.DeserializeObject<ImportCategoryDto[]>(inputJson);

        ICollection<Category> validCategories = new HashSet<Category>();

        foreach (var categoryDto in categoryDtos)
        {
            if (string.IsNullOrEmpty(categoryDto.Name))
            {
                continue;
            }

            var category = mapper.Map<Category>(categoryDto);
            validCategories.Add(category);
        }

        context.Categories.AddRange(validCategories);
        context.SaveChanges();

        return $"Successfully imported {validCategories.Count}";
    }

    // 04. Import Categories and Products 
    public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
    {
        var mapper = CreateMapper();

        ImportCategoryProductDto[] categoryProductDtos = JsonConvert.DeserializeObject<ImportCategoryProductDto[]>(inputJson);

        ICollection<CategoryProduct> categoryProducts = new HashSet<CategoryProduct>();

        foreach (var categoryProductDto in categoryProductDtos)
        {
            var categoryProduct = mapper.Map<CategoryProduct>(categoryProductDto);

            categoryProducts.Add(categoryProduct);
        }

        context.CategoriesProducts.AddRange(categoryProducts);
        context.SaveChanges();

        return $"Successfully imported {categoryProducts.Count}";
    }

    // 05.Export Products In Range
    public static string GetProductsInRange(ProductShopContext context)
    {
        var mapper = CreateMapper();

        ExportProductInRangeDto[] productDtos = context.Products
            .Where(p => p.Price >= 500 && p.Price <= 1000)
            .OrderBy(p => p.Price)
            .AsNoTracking()
            .ProjectTo<ExportProductInRangeDto>(mapper.ConfigurationProvider)
            .ToArray();

        return JsonConvert.SerializeObject(productDtos, Formatting.Indented);
    }

    // 06. Export Sold Products 
    public static string GetSoldProducts(ProductShopContext context)
    {
        var mapper = CreateMapper();

        ExportUserDto[] usersWithSoldProductsDtos = context.Users
            .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
            .OrderBy(u => u.LastName)
            .ThenBy(u => u.FirstName)
            .AsNoTracking()
            .ProjectTo<ExportUserDto>(mapper.ConfigurationProvider)
            .ToArray();

        return JsonConvert.SerializeObject(usersWithSoldProductsDtos, Formatting.Indented);
    }

    // 07. Export Categories By Products Count 
    public static string GetCategoriesByProductsCount(ProductShopContext context)
    {
        throw new NotImplementedException();
    }

    // 08. Export Users and Products 
    public static string GetUsersWithProducts(ProductShopContext context)
    {
        throw new NotImplementedException();
    }

    public static IMapper CreateMapper()
        => new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<ProductShopProfile>(); }));

    public static IContractResolver ConfigureCamelCaseNaming()
        => new DefaultContractResolver
            { NamingStrategy = new CamelCaseNamingStrategy(false, true) };
}