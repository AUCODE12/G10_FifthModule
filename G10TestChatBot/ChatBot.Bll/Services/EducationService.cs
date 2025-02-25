using ChatBot.Dal;
using ChatBot.Dal.Entites;

namespace ChatBot.Bll.Services;

public class EducationService : IEducationService
{
    private readonly MainContext mainContext;

    public EducationService(MainContext mainContext)
    {
        this.mainContext = mainContext;
    }

    public Task<long> AddEducationAsync(Education education)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Education>> GetEducationsByUserInfoIdAsync(long userInfoId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateEducationAsync(Education education)
    {
        throw new NotImplementedException();
    }
}
