using HotelApi.DTOs.Hotel;

namespace HotelApi.DTOs.Country;

public record GetCountryDto(
    int CountryId,
    string Name,
    string ShortName,
    List<GetHotelSlimDto> Hotels
);

public record GetCountriesDto(
    int CountryId,
    string Name,
    string ShortName
);