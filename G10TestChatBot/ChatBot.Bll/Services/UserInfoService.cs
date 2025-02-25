using ChatBot.Dal;
using ChatBot.Dal.Entites;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Bll.Services;

public class UserInfoService : IUserInfoService
{
    private readonly MainContext mainContext;

    public UserInfoService(MainContext mainContext)
    {
        this.mainContext = mainContext;
    }

    public async Task<long> AddUserInfoAsync(UserInfo userInfo)
    {
        try
        {
            await mainContext.UserInfos.AddAsync(userInfo);
            await mainContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return 0l;
        }

        return userInfo.UserInfoId;
    }

    public async Task<UserInfo> GetUserInfoByTelegramIdAsync(long telegramId)
    {
        var userInfo = await mainContext.UserInfos.FirstOrDefaultAsync(ui => ui.BotUserId == telegramId);
        return userInfo;
    }

    public Task<long> GetUserInfoIdByTelegramIdAsync(long telegramId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserInfoAsync(UserInfo userInfo)
    {
        throw new NotImplementedException();
    }
}
