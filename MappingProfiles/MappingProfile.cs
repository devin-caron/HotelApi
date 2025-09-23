using AutoMapper;
using HotelApi.Data;
using HotelApi.DTOs.Country;
using HotelApi.DTOs.Hotel;

namespace HotelApi.MappingProfiles;

public class HotelMappingProfile : Profile
{
    public HotelMappingProfile()
    {
        CreateMap<Hotel, GetHotelDto>()
            .ForMember(d => d.Country, cfg => cfg.MapFrom<CountryNameResolver>());
        CreateMap<Hotel, GetHotelSlimDto>(); // Added for Country -> GetCountryDto nested projection
        CreateMap<GetHotelDto, Hotel>();
        CreateMap<CreateHotelDto, Hotel>();
    }
}

public class CountryMappingProfile : Profile
{
    public CountryMappingProfile()
    {
        CreateMap<Country, GetCountryDto>();
        CreateMap<Country, GetCountriesDto>();
        CreateMap<CreateCountryDto, Country>();
    }
}

public class CountryNameResolver : IValueResolver<Hotel, GetHotelDto, string>
{
    public string Resolve(Hotel source, GetHotelDto destination, string destMember, ResolutionContext context)
    {
        return source.Country?.Name ?? string.Empty;
    }
}