using HotelApi.DTOs.Hotel;

namespace HotelApi.Contracts;

public interface IHotelsService
{
    Task<bool> HotelExistsAsync(int id);
    Task<bool> HotelExistsAsync(string name);
    Task<GetHotelDto> CreateHotelAsync(CreateHotelDto createHotelDto);
    Task DeleteHotelAsync(int id);
    Task<IEnumerable<GetHotelDto>> GetHotelsAsync();
    Task<GetHotelDto?> GetHotelAsync(int id);
    Task UpdateHotelAsync(int id, UpdateHotelDto updateHotelDto);
}