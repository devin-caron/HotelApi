using HotelApi.DTOs.Hotel;

namespace HotelApi.DTOs.Country;

public class GetCountryDto
{
    public int CountryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public List<GetHotelSlimDto> Hotels { get; set; } = new();
}

public class GetCountriesDto
{
    public int CountryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
}