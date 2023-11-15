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

        // Product 
        this.CreateMap<ImportProductDto, Product>();
        this.CreateMap<Product, ExportProductDto>()
            .ForMember(d => d.BuyerFullName,
                opt => opt.MapFrom(s => $"{s.Buyer.FirstName} {s.Buyer.LastName}"));

        // Category 
        this.CreateMap<ImportCategoryDto, Category>();

        // CategoryProduct
        this.CreateMap<ImportCategoryProductDto, CategoryProduct>();
    }
}