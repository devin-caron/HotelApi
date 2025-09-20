using System.ComponentModel.DataAnnotations;

namespace HotelApi.Data;

public class Country
{
    public int CountryId { get; set; }
    [MaxLength(20)]
    public required string Name { get; set; }
    [MaxLength(3)]
    public required string ShortName { get; set; }
    public IList<Hotel> Hotels { get; set; } = [];
}

