using Instagram.Dal;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Server.Configurations;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
        builder.Services.AddDbContext<MainContext>(o =>
          o.UseSqlServer(connectionString));
    }
}
