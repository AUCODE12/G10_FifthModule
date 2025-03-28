using ChatBot.Dal;
using ChatBot.Dal.Entites;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Bll.Services;

public class EducationService : IEducationService
{
    private readonly MainContext mainContext;

    public EducationService(MainContext mainContext)
    {
        this.mainContext = mainContext;
    }

    public async Task<long> AddEducationAsync(Education education)
    {
        await mainContext.Educations.AddAsync(education);
        await mainContext.SaveChangesAsync();
        return education.EducationId;
    }

    public async Task DeleteEducationAsync(long id, long userInfoId)
    {
        var education = await mainContext.Educations.FirstOrDefaultAsync(ed => ed.EducationId == id && ed.UserInfoId == userInfoId);

        if(education == null)
        {
            throw new Exception($"Education with {id} not found");
        }

        mainContext.Educations.Remove(education);
        await mainContext.SaveChangesAsync();
    }

    public async Task<ICollection<Education>> GetEducationsByUserInfoIdAsync(long userInfoId)
    {
        var educations = await mainContext.Educations
                            .Where(e => e.UserInfoId == userInfoId)
                            .ToListAsync();
        return educations ?? new List<Education>();
    }

    public Task UpdateEducationAsync(Education education)
    {
        throw new NotImplementedException();
    }
}
