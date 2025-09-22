using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelApi.Contracts;
using HotelApi.Data;
using HotelApi.DTOs.Hotel;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Services;

public class HotelsService(HotelDbContext context, IMapper mapper) : IHotelsService
{
    public async Task<IEnumerable<GetHotelDto>> GetHotelsAsync()
    {
        var hotels = await context.Hotels
            .Include(c => c.Country)
            .ProjectTo<GetHotelDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return hotels;
    }

    public async Task<GetHotelDto?> GetHotelAsync(int id)
    {
        var hotel = await context.Hotels
            .Where(h => h.Id == id)
            .Include(c => c.Country)
            .ProjectTo<GetHotelDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return hotel ?? null;
    }

    public async Task<GetHotelDto> CreateHotelAsync(CreateHotelDto createHotelDto)
    {
        var hotel = mapper.Map<Hotel>(createHotelDto);
        context.Hotels.Add(hotel);
        await context.SaveChangesAsync();

        var returnObj = mapper.Map<GetHotelDto>(hotel);

        return returnObj;
    }

    public async Task UpdateHotelAsync(int id, UpdateHotelDto updateHotelDto)
    {
        var hotel = await context.Hotels.FindAsync(id) ??
            throw new KeyNotFoundException("Hotel not found");

        hotel = mapper.Map<Hotel>(updateHotelDto);

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
