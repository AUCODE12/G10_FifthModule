using ChatBot.Dal.Entites;

namespace ChatBot.Bll.Services;

public interface IExperienceService
{
    Task<ICollection<Experience>> GetExperiencesByUserInfoIdAsync(long userInfoId);
    Task<long> AddExperienceAsync(Experience experience);
    Task UpdateExperienceAsync(Experience experience);
}