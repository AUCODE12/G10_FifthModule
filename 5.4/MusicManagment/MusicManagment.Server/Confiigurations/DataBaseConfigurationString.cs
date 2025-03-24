using Microsoft.EntityFrameworkCore;
using MusicManagement.DataAccess;
using MusicManagement.Repository.Settings;

namespace MusicManagement.Server.Confiigurations;

public static class DataBaseConfigurationString
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        var sqlDBConnectionString = new SqlDBConnectionString(connectionString);

        builder.Services.AddSingleton<SqlDBConnectionString>(sqlDBConnectionString);

        //builder.Services.AddDbContext<MainContext>(options =>
        //  options.UseSqlServer(connectionString));
    }
}
