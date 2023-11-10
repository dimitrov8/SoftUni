namespace CarDealer;

using AutoMapper;
using DTOs.Import;
using Models;

public class CarDealerProfile : Profile
{
    public CarDealerProfile()
    {
        // Supplier
        this.CreateMap<ImportSupplierDto, Supplier>();

        // Part
        this.CreateMap<ImportPartDto, Part>();

        // Car
        this.CreateMap<ImportCarDto, Car>();
        this.CreateMap<ImportCarDto, Car>()
            .ForMember(d => d.PartsCars,
                opt => opt.MapFrom(s =>
                    s.PartsId.Select(id => new PartCar { PartId = id })));
    }
}