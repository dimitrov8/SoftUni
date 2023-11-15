namespace ProductShop;

using AutoMapper;
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
        var context = new ProductShopContext();

        // Imports
        //string inputXml = File.ReadAllText("../../../Datasets/categories-products.xml");
        //string result = ImportCategoryProducts(context, inputXml);
        //Console.WriteLine(result);

        // Exports
        string result = GetProductsInRange(context);
        Console.WriteLine(result);
    }

    // 01. Import Users 
    public static string ImportUsers(ProductShopContext context, string inputXml)
    {
        var mapper = InitializeAutoMapper();
        var xmlHelper = new XmlHelper();
        ImportUserDto[] userDtos = xmlHelper.Deserialize<ImportUserDto[]>(inputXml, "Users");

        ICollection<User> validUsers = new HashSet<User>();

        foreach (var userDto in userDtos)
        {
            if (string.IsNullOrEmpty(userDto.FirstName) || string.IsNullOrEmpty(userDto.LastName))
            {
                continue;
            }

            // Manual mapping without AutoMapper
            //var user = new User
            //{
            //    FirstName = userDto.FirstName,
            //    LastName = userDto.LastName,
            //    Age = userDto.Age
            //};

            var user = mapper.Map<User>(userDto);
            validUsers.Add(user);
        }

        context.Users.AddRange(validUsers);
        context.SaveChanges();

        return $"Successfully imported {validUsers.Count}";
    }

    // 02. Import Products
    public static string ImportProducts(ProductShopContext context, string inputXml)
    {
        var mapper = InitializeAutoMapper();
        var xmlHelper = new XmlHelper();
        ImportProductDto[] productDtos = xmlHelper.Deserialize<ImportProductDto[]>(inputXml, "Products");

        ICollection<Product> validProducts = new HashSet<Product>();

        foreach (var productDto in productDtos)
        {
            if (string.IsNullOrEmpty(productDto.Name))
            {
                continue;
            }

            var product = mapper.Map<Product>(productDto);
            validProducts.Add(product);
        }

        context.Products.AddRange(validProducts);
        context.SaveChanges();

        return $"Successfully imported {validProducts.Count}";
    }

    // 03. Import Categories 
    public static string ImportCategories(ProductShopContext context, string inputXml)
    {
        var mapper = InitializeAutoMapper();
        var xmlHelper = new XmlHelper();
        ImportCategoryDto[] categoryDtos = xmlHelper.Deserialize<ImportCategoryDto[]>(inputXml, "Categories");

        var validCategories = new HashSet<Category>();

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
    public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
    {
        var mapper = InitializeAutoMapper();
        var xmlHelper = new XmlHelper();
        ImportCategoryProductDto[] categoryProductDtos = xmlHelper.Deserialize<ImportCategoryProductDto[]>(inputXml, "CategoryProducts");

        var validCategoryProducts = new HashSet<CategoryProduct>();

        foreach (var categoryProductDto in categoryProductDtos)
        {
            if (context.Categories.All(c => c.Id != categoryProductDto.CategoryId))
            {
                continue;
            }

            if (context.Products.All(p => p.Id != categoryProductDto.ProductId))
            {
                continue;
            }

            var categoryProduct = mapper.Map<CategoryProduct>(categoryProductDto);
            validCategoryProducts.Add(categoryProduct);
        }

        context.CategoryProducts.AddRange(validCategoryProducts);
        context.SaveChanges();

        return $"Successfully imported {validCategoryProducts.Count}";
    }

    // 05. Export Products In Range TODO =>
    public static string GetProductsInRange(ProductShopContext context)
    {
        var mapper = InitializeAutoMapper();
        var xmlHelper = new XmlHelper();

        Product[] products = context.Products
            .Include(p => p.Buyer) // Eager loading
            .Where(p => p.Price >= 500 && p.Price <= 1000)
            .OrderBy(p => p.Price)
            .Take(10)
            .AsNoTracking()
            .ToArray();

        var productDtos = new HashSet<ExportProductDto>();

        foreach (var product in products)
        {
            var productDto = mapper.Map<ExportProductDto>(product);
            productDtos.Add(productDto);
        }

        string serializeXml = xmlHelper.Serialize(productDtos, "Products");

        return serializeXml;
    }


    private static IMapper InitializeAutoMapper()
        => new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>()));
}