using ChatBot.Dal.Entites;

namespace ChatBot.Bll.UserService;

public interface IUserService
{
    Task AddUserAsync(TelegramUser user);
    Task UpdateUserAsync(TelegramUser user);
    Task<List<TelegramUser>> GetAllUsersAsync();
}