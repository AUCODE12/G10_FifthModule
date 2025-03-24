using OnlineVotingSystem.Dal.Entities;

namespace OnlineVotingSystem.Repository.Services;

public interface IUserRepository
{
    Task<long> AddUserAsync(User user);
    Task UpdateUserAsync(User updatedUser);
    Task DeleteUserAsync(long id);
    Task<User> GetUserByIdAsync(long id);
    Task<ICollection<User>> GetAllUsersAsync();
}