using ChatBot.Dal;
using ChatBot.Dal.Entites;
using Microsoft.EntityFrameworkCore;


namespace ChatBot.Bll.UserService;

public class UserService : IUserService
{
    private readonly MainContext mainContext;

    public UserService(MainContext mainContext)
    {
        this.mainContext = mainContext;
    }

    public async Task AddUserAsync(TelegramUser user)
    {
        var dbUser = await mainContext.Users.FirstOrDefaultAsync(x => x.TelegramUserId == user.TelegramUserId);

        if (dbUser != null)
        {
            Console.WriteLine($"User with id : {user.TelegramUserId} already exists");
            return;
        }
        
        try
        {
            await mainContext.Users.AddAsync(user);
            await mainContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public Task<List<TelegramUser>> GetAllUsersAsync()
    {
        var users = mainContext.Users.ToListAsync();
        return users;
    }

    public async Task UpdateUserAsync(TelegramUser user)
    {   
        var dbUser = await mainContext.Users.FirstOrDefaultAsync(x => x.TelegramUserId == user.TelegramUserId);
        dbUser = user;
        mainContext.Users.Update(dbUser);
        await mainContext.SaveChangesAsync();
    }
}
