using HotelApi.Contracts;
using HotelApi.Data;
using HotelApi.DTOs.Country;
using HotelApi.DTOs.Hotel;
using HotelApi.Results;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Services;

public class CountriesService(HotelDbContext context) : ICountriesService
{
    public async Task<Result<IEnumerable<GetCountriesDto>>> GetCountriesAsync()
    {
        var countries = await context.Countries
            .Select(c => new GetCountriesDto(c.CountryId, c.Name, c.ShortName))
            .ToListAsync();

        return Result<IEnumerable<GetCountriesDto>>.Success(countries);
    }

    public async Task<Result<GetCountryDto?>> GetCountryAsync(int id)
    {
        var country = await context.Countries
            .Where(c => c.CountryId == id)
            .Select(c => new GetCountryDto(c.CountryId, c.Name, c.ShortName,
                    c.Hotels.Select(h => new GetHotelSlimDto(h.Id, h.Name, h.Address, h.Rating
                    )).ToList()
            ))
            .FirstOrDefaultAsync();

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        return country is null
            ? Result<GetCountryDto>.NotFound()
            : Result<GetCountryDto>.Success(country);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
    }

    public async Task<Result<GetCountryDto>> CreateCountryAsync(CreateCountryDto createCountryDto)
    {
        try
        {
            var exists = await CountryExistsAsync(createCountryDto.Name);
            if (exists)
            {
                return Result<GetCountryDto>.Failure(new Error("Conflict", $"Country with name '{createCountryDto.Name}' already exists."));
            }

            var country = new Country
            {
                Name = createCountryDto.Name,
                ShortName = createCountryDto.ShortName
            };
            context.Countries.Add(country);
            await context.SaveChangesAsync();

            var dto = new GetCountryDto(
                country.CountryId,
                country.Name,
                country.ShortName,
                []
            );

            return Result<GetCountryDto>.Success(dto);
        }
        catch (Exception)
        {
            return Result<GetCountryDto>.Failure();
        }
    }

    public async Task<Result> UpdateCountryAsync(int id, UpdateCountryDto updateCountryDto)
    {
        try
        {
            if (id != updateCountryDto.CountryId)
            {
                return Result.BadRequest(new Error("Validation", "Id route does not match payload id."));
            }

            var country = await context.Countries.FindAsync(id);
            if (country is null)
            {
                return Result.NotFound(new Error("NotFound", $"Country - '{id}' was not found."));
            }

            var duplicateName = await CountryExistsAsync(updateCountryDto.Name);
            if (duplicateName)
            {
                return Result.Failure(new Error("Conflict", $"Country - '{updateCountryDto.Name}' already exists."));
            }

            country.Name = updateCountryDto.Name;
            country.ShortName = updateCountryDto.ShortName;
            context.Countries.Update(country);
            await context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure();
        }
    }

    public async Task<Result> DeleteCountryAsync(int id)
    {
        try
        {
            var country = await context.Countries.FindAsync(id);
            if (country is null)
            {
                return Result.NotFound(new Error("NotFound", $"Country - '{id}' was not found."));
            }

            context.Countries.Remove(country);
            await context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure();
        }
    }

    public async Task<bool> CountryExistsAsync(int id)
    {
        return await context.Countries.AnyAsync(e => e.CountryId == id);
    }

    public async Task<bool> CountryExistsAsync(string name)
    {
        return await context.Countries.AnyAsync(e => e.Name.ToLower().Trim() == name.ToLower().Trim());
    }
}