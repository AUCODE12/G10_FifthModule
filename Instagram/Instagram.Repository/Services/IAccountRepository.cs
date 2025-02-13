using Instagram.Dal.Entities;

namespace Instagram.Repository.Services;

public interface IAccountRepository
{
    Task<long> AddAccountAsync(Account account);
    Task<List<Account>> GetAllAccountsAsync();
}