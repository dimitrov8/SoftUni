namespace CarDealer;

using AutoMapper;
using DTOs.Export;
using DTOs.Import;
using Models;

public class CarDealerProfile : Profile
{
    public CarDealerProfile()
    {
        // Supplier
        this.CreateMap<ImportSupplierDto, Supplier>();
        this.CreateMap<Supplier, ExportLocalSupplierDto>()
            .ForMember(d => d.PartsCount,
                opt => opt.MapFrom(s => s.Parts.Count));

        // Sale 
        this.CreateMap<ImportSaleDto, Sale>();

        // Car
        this.CreateMap<Car, ExportCarDto>();
        this.CreateMap<Car, ExportCarMakeBmwDto>();
        this.CreateMap<Car, ExportCarAndPartsDto>()
            .ForMember(d => d.Parts,
                opt => opt.MapFrom(s => s.PartsCars
                    .Select(pc => pc.Part)));

        // Part
        this.CreateMap<PartCar, ExportPartDto>();
    }
}