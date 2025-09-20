using System.ComponentModel.DataAnnotations;

namespace HotelApi.DTOs.Hotel;

public class CreateHotelDto
{
    [MaxLength(20)]
    public required string Name { get; set; }
    [MaxLength(50)]
    public required string Address { get; set; }
    [Range(1, 5)]
    public double Rating { get; set; }

    [Required]
    public int CountryId { get; set; }
}