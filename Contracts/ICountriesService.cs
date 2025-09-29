using HotelApi.DTOs.Country;
using HotelApi.Result;

namespace HotelApi.Contracts;

public interface ICountriesService
{
    Task<bool> CountryExistsAsync(int id);
    Task<bool> CountryExistsAsync(string name);
    Task<Result<GetCountryDto>> CreateCountryAsync(CreateCountryDto createCountryDto);
    Task<Result.Result> DeleteCountryAsync(int id);
    Task<Result<IEnumerable<GetCountriesDto>>> GetCountriesAsync();
    Task<Result<GetCountryDto>> GetCountryAsync(int id);
    Task<Result.Result> UpdateCountryAsync(int id, UpdateCountryDto updateCountryDto);
}