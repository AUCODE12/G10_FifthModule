using ChatBot.Dal;
using ChatBot.Dal.Entites;

namespace ChatBot.Bll.Services;

public class SkillService : ISkillService
{
    private readonly MainContext mainContext;

    public SkillService(MainContext mainContext)
    {
        this.mainContext = mainContext;
    }

    public Task<long> AddSkillAsync(Skill skill)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Skill>> GetSkillsByUserInfoIdAsync(long userInfoId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateSkillAsync(Skill skill)
    {
        throw new NotImplementedException();
    }
}
