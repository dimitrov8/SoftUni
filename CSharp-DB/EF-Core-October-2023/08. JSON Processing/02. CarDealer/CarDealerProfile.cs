namespace CarDealer;

using AutoMapper;
using DTOs.Import;
using Models;

public class CarDealerProfile : Profile
{
    public CarDealerProfile()
    {
        // Car
        this.CreateMap<ImportCarDto, Car>();
        
        // Customer
        this.CreateMap<ImportCustomerDto, Customer>();

        // Sale 
        this.CreateMap<ImportSaleDto, Sale>()
            .ForMember(d => d.CarId,
                opt => opt.MapFrom(s => s.CarId))
            .ForMember(d => d.CustomerId,
                opt => opt.MapFrom(s => s.CustomerId))
            .ForMember(d => d.Discount,
                opt => opt.MapFrom(s => s.Discount));
    }
}