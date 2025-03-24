using Microsoft.EntityFrameworkCore;
using MusicCRUD.DataAccess;
using MusicCRUD.Repository.Settings;

namespace MusicCRUD.Server.Configurations;

public static class DataBaseConfiguration
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
        var sqlDBConeectionString = new SqlDBConeectionString(connectionString);

        builder.Services.AddSingleton<SqlDBConeectionString>(sqlDBConeectionString);
        builder.Services.AddDbContext<MainContext>(options =>
          options.UseSqlServer(connectionString));
    }
}
