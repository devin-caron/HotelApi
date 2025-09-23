using HotelApi.Contracts;
using HotelApi.DTOs.Country;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController(ICountriesService countriesService) : BaseApiController
{
    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountriesDto>>> GetCountries()
    {
        var countries = await countriesService.GetCountriesAsync();

        return ToActionResult(countries);
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCountryDto>> GetCountry(int id)
    {
        var country = await countriesService.GetCountryAsync(id);

        return ToActionResult(country);
    }

    // PUT: api/Countries/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
    {
        var result = await countriesService.UpdateCountryAsync(id, updateCountryDto);
        return ToActionResult(result);
    }

    // POST: api/Countries
    [HttpPost]
    public async Task<ActionResult<GetCountryDto>> PostCountry(CreateCountryDto createCountryDto)
    {
        var result = await countriesService.CreateCountryAsync(createCountryDto);
        if (!result.IsSuccess) return MapErrorsToResponse(result.Errors);

        return CreatedAtAction(nameof(GetCountry), new { id = result.Value!.CountryId }, result.Value);
    }

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var result = await countriesService.DeleteCountryAsync(id);

        return ToActionResult(result);
    }
}
