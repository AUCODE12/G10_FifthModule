using ChatBot.Dal;
using ChatBot.Dal.Entites;

namespace ChatBot.Bll.Services;

public class ExperienceService : IExperienceService
{
    private readonly MainContext mainContext;

    public ExperienceService(MainContext mainContext)
    {
        this.mainContext = mainContext;
    }

    public Task<long> AddExperienceAsync(Experience experience)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Experience>> GetExperiencesByUserInfoIdAsync(long userInfoId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateExperienceAsync(Experience experience)
    {
        throw new NotImplementedException();
    }
}
