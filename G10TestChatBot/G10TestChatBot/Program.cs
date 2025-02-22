using ChatBot.Bll.UserService;
using ChatBot.Dal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace G10TestChatBot;

internal class Program
{
    private static string BotToken = "7708587992:AAGGPGjZt9iEGec9iYGxbAEtvkivo5bhg4g";
    private static TelegramBotClient BotClient = new TelegramBotClient(BotToken);
    private static string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "UserIds.txt");
    //private static List<long> Ids = new List<long>();
    private static HashSet<long> Ids = new HashSet<long>();
    static async Task Main(string[] args)
    {
        var projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\.."));

        var configuration = new ConfigurationBuilder()
            .SetBasePath(projectDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddSingleton<BotListenerService>();
        serviceCollection.AddSingleton<MainContext>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var botListenerService = serviceProvider.GetRequiredService<BotListenerService>();
        await botListenerService.StartBot();

        Console.ReadKey();
    }

    public static void GG()
    {
        if (!File.Exists(FilePath))
        {
            File.WriteAllText(FilePath, "[]");
        }

        var receiverOptions = new ReceiverOptions { AllowedUpdates = new[] { UpdateType.Message, UpdateType.InlineQuery } };

        Console.WriteLine("Your bot is starting");

        BotClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync
            );

        Console.ReadKey();
    }



    static async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
    {
        Ids = await GetAllIds();
        var message = update.Message;
        var user = message.Chat;
        Console.WriteLine($"{user.Id},  {user.FirstName}, {message.Text}");

        if (user.Id != 1014326831) return;

        if (message.Text == "/start" && user.Id == 1014326831)
        {
            Ids.Add(user.Id);
            await SaveUserId();
            var keyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { new KeyboardButton("📍 Lokatsiyani yuborish") { RequestLocation = true } },
                new KeyboardButton[] { new KeyboardButton("📞 Telefon raqamni yuborish") { RequestContact = true } }
            });

            await bot.SendTextMessageAsync(user.Id, "keyboard", replyMarkup: keyboard, cancellationToken: cancellationToken);

            return;
        }

        if (message.Text.ToLower().Contains("salom"))
        {
            await bot.SendTextMessageAsync(user.Id, "salom nma gap", cancellationToken: cancellationToken);
            return;
        }

        if (message.Text.ToLower().StartsWith("hello") && user.Id == 1014326831)
        {
            var ids = await GetAllIds();
            foreach (var id in ids)
            {
                await bot.SendTextMessageAsync(id, message.Text, cancellationToken: cancellationToken);
            }
            return;
        }
    }
    static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {

    }

    public static async Task SaveUserId()
    {


        var json = JsonSerializer.Serialize(Ids);
        await File.WriteAllTextAsync(FilePath, json);
    }

    public static async Task<HashSet<long>> GetAllIds()
    {
        var idsString = File.ReadAllText(FilePath);
        var ids = JsonSerializer.Deserialize<HashSet<long>>(idsString);
        return ids ?? new HashSet<long>();
    }
}
