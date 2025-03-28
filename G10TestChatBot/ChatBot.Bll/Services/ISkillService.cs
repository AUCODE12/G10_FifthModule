using ChatBot.Dal.Entites;

namespace ChatBot.Bll.Services;

public interface ISkillService
{
    Task<ICollection<Skill>> GetSkillsByUserInfoIdAsync(long userInfoId);
    Task<long> AddSkillAsync(Skill skill);
    Task UpdateSkillAsync(Skill skill);
}