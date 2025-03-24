using AutoMapper;
using OnlineVotingSystem.Bll.Dtos;
using OnlineVotingSystem.Dal.Entities;
using OnlineVotingSystem.Repository.Services;

namespace OnlineVotingSystem.Bll.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<long> AddUserAsync(UserDto user)
    {
        var userMap = mapper.Map<User>(user);
        return await userRepository.AddUserAsync(userMap);
    }

    public async Task DeleteUserAsync(long id)
    {
        await userRepository.DeleteUserAsync(id);
    }

    public async Task<ICollection<UserDto>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllUsersAsync();
        var usersMapDto = users.Select(c => mapper.Map<UserDto>(c)).ToList();
        return usersMapDto;
    }

    public async Task<UserDto> GetUserByIdAsync(long id)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        return mapper.Map<UserDto>(user);
    }

    public async Task UpdateUserAsync(UserDto updatedUser)
    {
        var user = mapper.Map<User>(updatedUser);
        await userRepository.UpdateUserAsync(user);
    }
}
