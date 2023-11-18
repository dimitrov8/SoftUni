namespace ProductShop;

using AutoMapper;
using DTOs.Export;
using DTOs.Import;
using Models;

public class ProductShopProfile : Profile
{
    public ProductShopProfile()
    {
        // User
        this.CreateMap<ImportUserDto, User>();

        this.CreateMap<User, ExportUserSoldProductDto>()
            .ForMember(d => d.SoldProducts,
                opt => opt.MapFrom(s => s.ProductsSold));

        // Product 
        this.CreateMap<ImportProductDto, Product>();

        this.CreateMap<Product, ExportProductDto>()
            .ForMember(d => d.BuyerFullName,
                opt => opt.MapFrom(s => $"{s.Buyer.FirstName} {s.Buyer.LastName}"));

        this.CreateMap<Product, ExportUserSoldProductDtoNested>();

        // Category 
        this.CreateMap<ImportCategoryDto, Category>();

        this.CreateMap<Category, ExportCategoryByProductCountDto>()
            .ForMember(d => d.Count,
                opt => opt.MapFrom(s => s.CategoryProducts.Count))
            .ForMember(d => d.Average,
                opt => opt.MapFrom(s => s.CategoryProducts.Average(p => p.Product.Price)))
            .ForMember(d => d.TotalRevenue, 
                opt => opt.MapFrom(c => c.CategoryProducts.Sum(p => p.Product.Price)));

            // CategoryProduct
        this.CreateMap<ImportCategoryProductDto, CategoryProduct>();
    }
}