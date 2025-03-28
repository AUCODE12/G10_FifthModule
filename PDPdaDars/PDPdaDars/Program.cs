using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

class Program
{
    private static TelegramBotClient botClient;
    private static Queue<long> waitingUsers = new Queue<long>();
    private static Dictionary<long, long> activeChats = new Dictionary<long, long>();

    static async Task Main(string[] args)
    {
        string token = "7487221158:AAEcolrntt9L-FV9ocEYbPqKJq_UNQo-dNA";
        botClient = new TelegramBotClient(token);

        botClient.StartReceiving(UpdateHandler, ErrorHandler);

        Console.WriteLine("Bot is running...");
        Console.ReadLine();
    }

    private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.Message && update.Message.Text != null)
        {
            Console.WriteLine(update.Message.Chat.FirstName + " " + update.Message.Text);
            long userId = update.Message.Chat.Id;
            string text = update.Message.Text;

            if (text == "/start")
            {
                await SendStartMenu(userId);
            }
            else if (text == "Start Chatting")
            {
                await HandleStartChatting(userId);
            }
            else if (text == "Stop Chatting")
            {
                await HandleStopChatting(userId);
            }
            else
            {
                await ForwardMessage(userId, text);
            }
        }
    }

    private static async Task SendStartMenu(long userId)
    {
        var replyMarkup = new ReplyKeyboardMarkup(new[]
        {
            new KeyboardButton("Start Chatting"),
            new KeyboardButton("Stop Chatting")
        })
        {
            ResizeKeyboard = true
        };

        await botClient.SendTextMessageAsync(userId, "Welcome! Press 'Start Chatting' to begin.", replyMarkup: replyMarkup);
    }

    private static async Task HandleStartChatting(long userId)
    {
        if (activeChats.ContainsKey(userId))
        {
            await botClient.SendTextMessageAsync(userId, "You are already in a chat.");
            return;
        }

        if (waitingUsers.Count > 0)
        {
            long pairedUserId = waitingUsers.Dequeue();
            activeChats[userId] = pairedUserId;
            activeChats[pairedUserId] = userId;

            await botClient.SendTextMessageAsync(userId, "You are now connected to a stranger.");
            await botClient.SendTextMessageAsync(pairedUserId, "You are now connected to a stranger.");
        }
        else
        {
            waitingUsers.Enqueue(userId);
            await botClient.SendTextMessageAsync(userId, "Waiting for a stranger to connect...");
        }
    }

    private static async Task HandleStopChatting(long userId)
    {
        if (activeChats.ContainsKey(userId))
        {
            long pairedUserId = activeChats[userId];
            activeChats.Remove(userId);
            activeChats.Remove(pairedUserId);

            await botClient.SendTextMessageAsync(userId, "You have left the chat.");
            await botClient.SendTextMessageAsync(pairedUserId, "Your chat partner has left the chat.");

            await SendStartMenu(userId);
            await SendStartMenu(pairedUserId);
        }
        else
        {
            await botClient.SendTextMessageAsync(userId, "You are not in a chat.");
        }
    }

    private static async Task ForwardMessage(long userId, string text)
    {
        if (activeChats.ContainsKey(userId))
        {
            long pairedUserId = activeChats[userId];
            await botClient.SendTextMessageAsync(pairedUserId, text);
        }
        else
        {
            await botClient.SendTextMessageAsync(userId, "You are not in a chat. Press 'Start Chatting' to begin.");
        }
    }

    private static Task ErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Error: {exception.Message}");
        return Task.CompletedTask;
    }
}