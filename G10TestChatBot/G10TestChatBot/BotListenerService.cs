using ChatBot.Bll.UserService;
using ChatBot.Dal.Entites;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace G10TestChatBot;

public class BotListenerService
{
    private static string botToken = "7708587992:AAGGPGjZt9iEGec9iYGxbAEtvkivo5bhg4g";
    private TelegramBotClient botClient = new TelegramBotClient(botToken);
    private readonly IUserService userService;

    public BotListenerService(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task StartBot()
    {
        botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync
            );

        Console.WriteLine("Bot is runing");

        Console.ReadKey();
    }

    private async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.Message)
        {
            var user = update.Message.Chat;
            var message = update.Message;

            if (message.Text == "/start")
            {
                var savingUser = new TelegramUser()
                {
                    TelegramUserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    UpdatedAt = DateTime.UtcNow,
                };

                await userService.AddUserAsync(savingUser);

                //await bot.SendTextMessageAsync(user.Id, "You are successfully started");

                var menu = new ReplyKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new KeyboardButton("Opt1"),
                        new KeyboardButton("Opt2")
                    },
                    new[]
                    {
                        new KeyboardButton("Opt1"),
                        new KeyboardButton("Opt2"),
                        new KeyboardButton("Opt3")
                    },
                    new[]
                    {
                        new KeyboardButton("Opt1"),
                        new KeyboardButton("Opt2"),
                        new KeyboardButton("Opt3"),
                        new KeyboardButton("Opt4")
                    }
                })
                {
                    ResizeKeyboard = true,
                    OneTimeKeyboard = true
                };


                await bot.SendTextMessageAsync(user.Id, "You are successfully started", replyMarkup: menu);

                return;
            }

            if(message.Text == "Opt1" || message.Text == "Opt2")
            {
                var inlineMenu = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Opt1", "opt1"),
                        InlineKeyboardButton.WithCallbackData("Opt2", "opt2")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Opt1", "opt1"),
                        InlineKeyboardButton.WithCallbackData("Opt2", "opt2"),
                        InlineKeyboardButton.WithCallbackData("Opt3", "opt3")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Opt1", "opt1"),
                        InlineKeyboardButton.WithCallbackData("Opt2", "opt2"),
                        InlineKeyboardButton.WithCallbackData("Opt3", "opt3"),
                        InlineKeyboardButton.WithCallbackData("Opt4", "opt4")
                    }
                });

                await bot.SendTextMessageAsync(user.Id, "Choose an option:", replyMarkup: inlineMenu);
            }




        }
        else if (update.Type == UpdateType.CallbackQuery)
        {
            var id1 = update.CallbackQuery.Id;
            var id2 = update.CallbackQuery.InlineMessageId;
            var id = update.CallbackQuery.From.Id;

            CallbackQuery res = update.CallbackQuery;

            var rep = update.CallbackQuery.Data;


            await bot.SendTextMessageAsync(id, $"your option : {update.CallbackQuery.Data}");
        }
    }



    private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {

    }
}
