using ChatBot.Dal.Entites;

namespace ChatBot.Bll.Services;

public interface IUserInfoService
{
    Task<long> AddUserInfoAsync(UserInfo userInfo);
    Task UpdateUserInfoAsync(UserInfo userInfo);
    Task<long> GetUserInfoIdByTelegramIdAsync(long telegramId);
    Task<UserInfo> GetUserInfoByTelegramIdAsync(long telegramId);
}