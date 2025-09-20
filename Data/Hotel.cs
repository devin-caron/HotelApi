﻿using System.ComponentModel.DataAnnotations;

namespace HotelApi.Data;

public class Hotel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    [MaxLength(50)]
    public required string Address { get; set; }
    [Range(1, 5)]
    public double Rating { get; set; }

    [Required]
    public int CountryId { get; set; }
    public Country? Country { get; set; }
}