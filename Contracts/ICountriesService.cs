using HotelApi.DTOs.Country;

namespace HotelApi.Contracts;

public interface ICountriesService
{
    Task<bool> CountryExistsAsync(int id);
    Task<bool> CountryExistsAsync(string name);
    Task<GetCountryDto> CreateCountryAsync(CreateCountryDto createCountryDto);
    Task DeleteCountryAsync(int id);
    Task<IEnumerable<GetCountriesDto>> GetCountriesAsync();
    Task<GetCountryDto?> GetCountryAsync(int id);
    Task UpdateCountryAsync(int id, UpdateCountryDto updateCountryDto);
}