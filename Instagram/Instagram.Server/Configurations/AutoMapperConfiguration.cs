using Instagram.Bll.MappingProfiles;

namespace Instagram.Server.Configurations;

public static class AutoMapperConfiguration
{
    public static void ConfigureAutoMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(CommentProfile));
    }
}
