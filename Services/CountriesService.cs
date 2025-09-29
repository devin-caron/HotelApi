using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelApi.Constants;
using HotelApi.Contracts;
using HotelApi.Data;
using HotelApi.DTOs.Country;
using HotelApi.Result;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Services;

public class CountriesService(HotelDbContext context, IMapper mapper) : ICountriesService
{
    public async Task<Result<IEnumerable<GetCountriesDto>>> GetCountriesAsync()
    {
        var countries = await context.Countries
            .ProjectTo<GetCountriesDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return Result<IEnumerable<GetCountriesDto>>.Success(countries);
    }

    public async Task<Result<GetCountryDto>> GetCountryAsync(int id)
    {
        var country = await context.Countries
            .Where(q => q.CountryId == id)
            .ProjectTo<GetCountryDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return country is null
            ? Result<GetCountryDto>.Failure(new Error(ErrorCodes.NotFound, $"Country '{id}' was not found."))
            : Result<GetCountryDto>.Success(country);
    }

    public async Task<Result<GetCountryDto>> CreateCountryAsync(CreateCountryDto createCountryDto)
    {
        try
        {
            var exists = await CountryExistsAsync(createCountryDto.Name);
            if (exists)
            {
                return Result<GetCountryDto>.Failure(new Error(ErrorCodes.Conflict, $"Country with name '{createCountryDto.Name}' already exists."));
            }

            var country = mapper.Map<Country>(createCountryDto);
            context.Countries.Add(country);
            await context.SaveChangesAsync();

            var dto = await context.Countries
                .Where(c => c.CountryId == country.CountryId)
                .ProjectTo<GetCountryDto>(mapper.ConfigurationProvider)
                .FirstAsync();

            return Result<GetCountryDto>.Success(dto);
        }
        catch (Exception)
        {
            return Result<GetCountryDto>.Failure(new Error(ErrorCodes.Failure, "An unexpected error occured while creating country"));
        }
    }

    public async Task<Result.Result> UpdateCountryAsync(int id, UpdateCountryDto updateCountryDto)
    {
        try
        {
            if (id != updateCountryDto.CountryId)
            {
                return Result.Result.BadRequest(new Error(ErrorCodes.Validation, "Id route does not match payload id."));
            }

            var country = await context.Countries.FindAsync(id);
            if (country is null)
            {
                return Result.Result.NotFound(new Error(ErrorCodes.NotFound, $"Country - '{id}' was not found."));
            }

            var duplicateName = await CountryExistsAsync(updateCountryDto.Name);
            if (duplicateName)
            {
                return Result.Result.Failure(new Error(ErrorCodes.Conflict, $"Country - '{updateCountryDto.Name}' already exists."));
            }

            mapper.Map(updateCountryDto, country);
            context.Countries.Update(country);
            await context.SaveChangesAsync();

            return Result.Result.Success();
        }
        catch (Exception)
        {
            return Result.Result.Failure(new Error(ErrorCodes.Failure, "An unexpected error occurred while updating the country."));
        }
    }

    public async Task<Result.Result> DeleteCountryAsync(int id)
    {
        try
        {
            var country = await context.Countries.FindAsync(id);
            if (country is null)
            {
                return Result.Result.NotFound(new Error(ErrorCodes.NotFound, $"Country - '{id}' was not found."));
            }

            context.Countries.Remove(country);
            await context.SaveChangesAsync();

            return Result.Result.Success();
        }
        catch (Exception)
        {
            return Result.Result.Failure(new Error(ErrorCodes.Failure, "An unexpected error occurred while deleting the country."));
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