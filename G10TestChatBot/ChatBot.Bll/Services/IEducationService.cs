using ChatBot.Dal.Entites;

namespace ChatBot.Bll.Services;

public interface IEducationService
{
    Task<ICollection<Education>> GetEducationsByUserInfoIdAsync(long userInfoId);
    Task<long> AddEducationAsync(Education education);
    Task UpdateEducationAsync(Education education);
}