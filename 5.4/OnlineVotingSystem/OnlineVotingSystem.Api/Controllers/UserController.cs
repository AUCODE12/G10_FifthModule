using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Bll.Dtos;
using OnlineVotingSystem.Bll.Services;

namespace OnlineVotingSystem.Api.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost("add")]
    public async Task<long> PostUser(UserDto userDto)
    {
        return await userService.AddUserAsync(userDto);
    }

    [HttpPut("update")]
    public async Task PutUser(UserDto userDto)
    {
        await userService.UpdateUserAsync(userDto);
    }

    [HttpDelete("delete")]
    public async Task DeleteUser(long id)
    {
        await userService.DeleteUserAsync(id);
    }

    [HttpGet("get")]
    public async Task<ICollection<UserDto>> GetAllUsers()
    {
        return await userService.GetAllUsersAsync();
    }

    [HttpGet("getById")]
    public async Task<UserDto> GetUserById(long id)
    {
        return await userService.GetUserByIdAsync(id);
    }
}
