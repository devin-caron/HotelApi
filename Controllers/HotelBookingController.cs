using HotelApi.DTOs.Booking;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers;

[Route("api/hotels/{hotelId:int}/bookings")]
[ApiController]
public class HotelBookingController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<GetBookingDto>> GetBookings([FromRoute] int hotelId)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<GetBookingDto>> CreateBookings([FromRoute] int hotelId, [FromBody] CreateBookingDto createBookingDto)
    {
        return Ok();
    }
}
