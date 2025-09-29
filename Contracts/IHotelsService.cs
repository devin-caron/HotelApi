using HotelApi.DTOs.Hotel;
using HotelApi.Result;

namespace HotelApi.Contracts;

public interface IHotelsService
{
    Task<bool> HotelExistsAsync(int id);
    Task<bool> HotelExistsAsync(string name, int countryId);
    Task<Result<IEnumerable<GetHotelDto>>> GetHotelsAsync();
    Task<Result<GetHotelDto>> GetHotelAsync(int id);
    Task<Result<GetHotelDto>> CreateHotelAsync(CreateHotelDto createHotelDto);
    Task<Result.Result> UpdateHotelAsync(int id, UpdateHotelDto updateHotelDto);
    Task<Result.Result> DeleteHotelAsync(int id);
}