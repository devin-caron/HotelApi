using HotelApi.Contracts;
using HotelApi.Data;
using HotelApi.DTOs.Country;
using HotelApi.DTOs.Hotel;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Services;

public class CountriesService(HotelDbContext context) : ICountriesService
{
    public async Task<IEnumerable<GetCountriesDto>> GetCountriesAsync()
    {
        return await context.Countries
            .Select(c => new GetCountriesDto(c.CountryId, c.Name, c.ShortName))
            .ToListAsync();
    }

    public async Task<GetCountryDto?> GetCountryAsync(int id)
    {
        var country = await context.Countries
            .Where(c => c.CountryId == id)
            .Select(c => new GetCountryDto(c.CountryId, c.Name, c.ShortName,
                    c.Hotels.Select(h => new GetHotelSlimDto(h.Id, h.Name, h.Address, h.Rating
                    )).ToList()
            ))
            .FirstOrDefaultAsync();

        return country ?? null;
    }

    public async Task<GetCountryDto> CreateCountryAsync(CreateCountryDto createCountryDto)
    {
        var country = new Country
        {
            Name = createCountryDto.Name,
            ShortName = createCountryDto.ShortName
        };
        context.Countries.Add(country);
        await context.SaveChangesAsync();

        return new GetCountryDto(
            country.CountryId,
            country.Name,
            country.ShortName,
            []
        );
    }

    public async Task UpdateCountryAsync(int id, UpdateCountryDto updateCountryDto)
    {
        var country = await context.Countries.FindAsync(id) ??
            throw new KeyNotFoundException("Country not found");
        country.Name = updateCountryDto.Name;
        country.ShortName = updateCountryDto.ShortName;
        context.Countries.Update(country);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCountryAsync(int id)
    {
        //var country = await context.Countries.FindAsync(id) ??
        //        throw new KeyNotFoundException("Country not found");
        //context.Countries.Remove(country);
        //await context.SaveChangesAsync();

        var country = await context.Countries
            .Where(c => c.CountryId == id)
            .ExecuteDeleteAsync();
    }

    public async Task<bool> CountryExistsAsync(int id)
    {
        return await context.Countries.AnyAsync(e => e.CountryId == id);
    }

    public async Task<bool> CountryExistsAsync(string name)
    {
        return await context.Countries.AnyAsync(e => e.Name == name);
    }
}
