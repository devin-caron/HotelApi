using HotelApi.DTOs.Auth;
using HotelApi.Result;

namespace HotelApi.Contracts;

public interface IUsersService
{
    Task<Result<string>> LoginAsync(LoginUserDto dto);
    Task<Result<RegisteredUserDto>> RegisterAsync(RegisterUserDto registerUserDto);
}