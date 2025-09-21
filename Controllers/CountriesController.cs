using HotelApi.Contracts;
using HotelApi.DTOs.Country;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController(ICountriesService countriesService) : ControllerBase
{
    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountriesDto>>> GetCountries()
    {
        var countries = await countriesService.GetCountriesAsync();

        return Ok(countries);
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCountryDto>> GetCountry(int id)
    {
        var country = await countriesService.GetCountryAsync(id);

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

        await countriesService.UpdateCountryAsync(id, updateCountryDto);

        return NoContent();
    }

    // POST: api/Countries
    [HttpPost]
    public async Task<ActionResult<GetCountryDto>> PostCountry(CreateCountryDto createCountryDto)
    {
        var resultDto = await countriesService.CreateCountryAsync(createCountryDto);

        return CreatedAtAction(nameof(GetCountry), new { id = resultDto.CountryId }, resultDto);
    }

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        await countriesService.DeleteCountryAsync(id);

        return NoContent();
    }
}
