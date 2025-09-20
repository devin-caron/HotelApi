using System.ComponentModel.DataAnnotations;

namespace HotelApi.DTOs.Hotel;

public class UpdateHotelDto : CreateHotelDto
{
    [Required]
    public int Id { get; set; }
}
