using System.ComponentModel.DataAnnotations;

namespace HotelApi.DTOs.Country;

public class CreateCountryDto
{
    [MaxLength(60)]
    public required string Name { get; set; }
    [MaxLength(3)]
    public required string ShortName { get; set; }
}
