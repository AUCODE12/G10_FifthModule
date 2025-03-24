using OnlineVotingSystem.Repository.Settings;

namespace OnlineVotingSystem.Api.Configurations;

public static class DataBaseConfiguration
{
    public static void ConfigureDatasase(this WebApplicationBuilder web)
    {
        var connectionString = web.Configuration.GetConnectionString("DatabaseConnection");
        var sqlDBConnectionString = new SqlDBConnectionString(connectionString);

        web.Services.AddSingleton<SqlDBConnectionString>(sqlDBConnectionString);
    }
}
