using ChatBot.Bll.Services;
using ChatBot.Dal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace G10TestChatBot;

internal class Program
{
    static async Task Main(string[] args)
    {
        var projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\.."));

        var configuration = new ConfigurationBuilder()
            .SetBasePath(projectDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddScoped<IBotUserService, BotUserService>();
        serviceCollection.AddScoped<IEducationService, EducationService>();
        serviceCollection.AddScoped<IExperienceService, ExperienceService>();
        serviceCollection.AddScoped<ISkillService, SkillService>();
        serviceCollection.AddScoped<IUserInfoService, UserInfoService>();
        serviceCollection.AddScoped<IFileService, PdfService>();
        serviceCollection.AddSingleton<BotListenerService>();
        serviceCollection.AddSingleton<MainContext>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var botListenerService = serviceProvider.GetRequiredService<BotListenerService>();
        await botListenerService.StartBot();

        Console.ReadKey();
    }
}
