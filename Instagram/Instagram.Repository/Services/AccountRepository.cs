using Instagram.Dal;
using Instagram.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Repository.Services;

public class AccountRepository : IAccountRepository
{
    private readonly MainContext MainContext;

    public AccountRepository(MainContext mainContext)
    {
        MainContext = mainContext;
    }

    public async Task<long> AddAccountAsync(Account account)
    {
        await MainContext.Accounts.AddAsync(account);
        await MainContext.SaveChangesAsync();
        return account.AccountId;
    }

    public async Task<List<Account>> GetAllAccountsAsync()
    {
        return await MainContext.Accounts
            .Include(a => a.Posts)
            .Include(a => a.Comments)
            .ToListAsync();
    }
}
