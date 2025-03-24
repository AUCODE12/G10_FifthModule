using OnlineVotingSystem.Bll.MappingProfiles;

namespace OnlineVotingSystem.Api.Configurations;

public static class AutoMapperConfiguration
{
    public static void ConfigureAutoMappers(this WebApplicationBuilder web)
    {
        web.Services.AddAutoMapper(typeof(UserProfile));
    }
}
