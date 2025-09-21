using HotelApi.Contracts;
using HotelApi.Data;
using HotelApi.DTOs.Hotel;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Services;

public class HotelsService(HotelDbContext context) : IHotelsService
{
    public async Task<IEnumerable<GetHotelDto>> GetHotelsAsync()
    {
        var hotels = await context.Hotels
            .Include(c => c.Country)
            .Select(h => new GetHotelDto(h.Id, h.Name, h.Address, h.Rating, h.CountryId, h.Country!.Name))
            .ToListAsync();
        return hotels;
    }

    public async Task<GetHotelDto?> GetHotelAsync(int id)
    {
        var hotel = await context.Hotels
            .Where(h => h.Id == id)
            .Select(h => new GetHotelDto(h.Id, h.Name, h.Address, h.Rating, h.CountryId, h.Country!.Name))
            .FirstOrDefaultAsync();

        return hotel ?? null;
    }

    public async Task<GetHotelDto> CreateHotelAsync(CreateHotelDto createHotelDto)
    {
        var hotel = new Hotel
        {
            Name = createHotelDto.Name,
            Address = createHotelDto.Address,
            Rating = createHotelDto.Rating,
            CountryId = createHotelDto.CountryId,
        };

        context.Hotels.Add(hotel);
        await context.SaveChangesAsync();

        return new GetHotelDto(
            hotel.Id,
            hotel.Name,
            hotel.Address,
            hotel.Rating,
            hotel.CountryId,
            string.Empty // no country name here
        );
    }

    public async Task UpdateHotelAsync(int id, UpdateHotelDto updateHotelDto)
    {
        var hotel = await context.Hotels.FindAsync(id) ??
            throw new KeyNotFoundException("Hotel not found");

        hotel.Name = updateHotelDto.Name;
        hotel.Address = updateHotelDto.Address;
        hotel.Rating = updateHotelDto.Rating;
        hotel.CountryId = updateHotelDto.CountryId;

        context.Entry(hotel).State = EntityState.Modified;

        context.Hotels.Update(hotel);
        await context.SaveChangesAsync();
    }

    public async Task DeleteHotelAsync(int id)
    {
        var hotel = await context.Hotels
            .Where(h => h.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<bool> HotelExistsAsync(int id)
    {
        return await context.Hotels.AnyAsync(e => e.Id == id);
    }

    public async Task<bool> HotelExistsAsync(string name)
    {
        return await context.Hotels.AnyAsync(e => e.Name == name);
    }
}
