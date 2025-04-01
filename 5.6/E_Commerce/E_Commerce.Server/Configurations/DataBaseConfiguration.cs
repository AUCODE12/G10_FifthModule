using E_Commerce.Dal;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Server.Configurations;

public static class DataBaseConfiguration
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        //builder.Services.AddDbContext<MainContext>(options =>
        //    options.UseSqlServer(connectionString));

        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string is missing or empty!");
        }

        builder.Services.AddDbContext<MainContext>(options =>
            options.UseSqlServer(connectionString));    
    }
}
