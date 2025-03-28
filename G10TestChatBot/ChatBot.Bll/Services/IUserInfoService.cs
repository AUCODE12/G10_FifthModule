using ChatBot.Dal.Entites;

namespace ChatBot.Bll.Services;

public interface IUserInfoService
{
    Task<long> AddUserInfoAsync(UserInfo userInfo);
    Task UpdateUserInfoAsync(UserInfo userInfo);
    Task<long> GetUserInfoIdByBotUserIdAsync(long botUserId);
    Task<UserInfo> GetUserInfoByBotUserIdAsync(long botUserId);
}