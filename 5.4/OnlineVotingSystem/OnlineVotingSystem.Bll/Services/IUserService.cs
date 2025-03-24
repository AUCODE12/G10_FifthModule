using OnlineVotingSystem.Bll.Dtos;

namespace OnlineVotingSystem.Bll.Services;

public interface IUserService
{
    Task<long> AddUserAsync(UserDto user);
    Task UpdateUserAsync(UserDto updatedUser);
    Task DeleteUserAsync(long id);
    Task<UserDto> GetUserByIdAsync(long id);
    Task<ICollection<UserDto>> GetAllUsersAsync();
}