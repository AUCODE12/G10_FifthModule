
using Microsoft.EntityFrameworkCore;
using MusicManagement.DataAccess;
using MusicManagement.Repository.Services;
using MusicManagement.Service.Sevices;

namespace MusicManagment.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IMusicService, MusicService>();
        //builder.Services.AddSingleton<IMusicRepository, MusicBrokerFile>(); 
        //builder.Services.AddScoped<IMusicRepository, MusicRepository>(); //sql
        builder.Services.AddScoped<IMusicRepository, MusicRepositoryAdoNet>(); //sql
        builder.Services.AddSingleton<MainContext>();

        //var connectionString = builder.Configuration.GetConnectionString("myConxStr");

        //// DbContext ni ro‘yxatga olish
        //builder.Services.AddDbContext<MainContext>(options =>
        //    options.UseSqlServer(connectionString));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
