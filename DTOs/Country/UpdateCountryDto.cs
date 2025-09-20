using System.ComponentModel.DataAnnotations;

namespace HotelApi.DTOs.Country;

public class UpdateCountryDto : CreateCountryDto
{
    [Required]
    public int CountryId { get; set; }
}
