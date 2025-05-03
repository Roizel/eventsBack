using EventTrackingSystem.Application.Common.DTOs;

namespace EventTrackingSystem.Application.Common.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(int id);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<string> AddUserAsync(SignUpDto item);
    Task DeleteUserAsync(int id);
    Task<string> SignInAsync(SignInDto item);
}
