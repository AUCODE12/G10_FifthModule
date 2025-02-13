using Instagram.Bll.Services;
using Instagram.Dal;
using Instagram.Repository.Services;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Server.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void ConfigureDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();
        builder.Services.AddScoped<ICommentService, CommentService>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<IPostService, PostService>();
    }
}
