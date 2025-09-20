using HotelApi.Data;
using HotelApi.DTOs.Country;
using HotelApi.DTOs.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly HotelDbContext _context;

    public CountriesController(HotelDbContext context)
    {
        _context = context;
    }

    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountriesDto>>> GetCountries()
    {
        var countries = await _context.Countries
            .Select(c => new GetCountriesDto(
                    c.CountryId,
                    c.Name,
                    c.ShortName
                ))
            .ToListAsync();

        return Ok(countries);
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCountryDto>> GetCountry(int id)
    {
        var country = await _context.Countries
            .Where(c => c.CountryId == id)
            .Select(c => new GetCountryDto(
                    c.CountryId,
                    c.Name,
                    c.ShortName,
                    c.Hotels.Select(h => new GetHotelSlimDto(
                        h.Id,
                        h.Name,
                        h.Address,
                        h.Rating
                    )).ToList()
            ))
            .FirstOrDefaultAsync();

        if (country == null)
        {
            return NotFound();
        }

        return Ok(country);
    }

    // PUT: api/Countries/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
    {
        if (id != updateCountryDto.CountryId)
        {
            return BadRequest();
        }

        var country = await _context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }
        country.Name = updateCountryDto.Name;
        country.ShortName = updateCountryDto.ShortName;


        _context.Entry(country).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await CountryExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Countries
    [HttpPost]
    public async Task<ActionResult<GetCountryDto>> PostCountry(CreateCountryDto createCountryDto)
    {
        var country = new Country
        {
            Name = createCountryDto.Name,
            ShortName = createCountryDto.ShortName
        };

        _context.Countries.Add(country);
        await _context.SaveChangesAsync();

        var resultDto = new GetCountryDto(
            country.CountryId,
            country.Name,
            country.ShortName,
            []
        );

        return CreatedAtAction(nameof(GetCountry), new { id = country.CountryId }, resultDto);
    }

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var country = await _context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> CountryExists(int id)
    {
        return await _context.Countries.AnyAsync(e => e.CountryId == id);
    }
}
