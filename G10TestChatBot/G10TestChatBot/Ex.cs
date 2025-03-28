using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

class BBB
{
    private static ITelegramBotClient _botClient;
    //private static Dictionary<long, UserState> _userStates = new Dictionary<long, UserState>();
    private static Queue<long> _waitingUsers = new Queue<long>();
    private static Dictionary<long, long> _activeChats = new Dictionary<long, long>();

    //static async Task Main(string[] args)
    //{
    //    _botClient = new TelegramBotClient("7708587992:AAGGPGjZt9iEGec9iYGxbAEtvkivo5bhg4g");

    //    // Start receiving updates
    //    var cts = new CancellationTokenSource();
    //    var receiverOptions = new ReceiverOptions
    //    {
    //        AllowedUpdates = Array.Empty<UpdateType>() // Receive all update types
    //    };

    //    _botClient.StartReceiving(
    //        updateHandler: HandleUpdateAsync,
    //        errorHandler: HandleErrorAsync,
    //        receiverOptions: receiverOptions,
    //        cancellationToken: cts.Token
    //    );

    //    Console.WriteLine("Bot started. Press any key to exit...");
    //    Console.ReadKey();

    //    // Stop receiving updates
    //    cts.Cancel();
    //}

    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Only process text messages
        if (update.Type != UpdateType.Message || update.Message.Type != MessageType.Text)
            return;

        var message = update.Message;
        var userId = message.Chat.Id;
        var text = message.Text;

        if (text == "/start")
        {
            await SendStartMenu(userId);
        }
        else if (text == "Start Chatting")
        {
            await StartChatting(userId);
        }
        else if (text == "Stop Chatting")
        {
            await StopChatting(userId);
        }
        else
        {
            await RelayMessage(userId, text);
        }
    }

    private static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Error: {exception.Message}");
    }

    private static async Task SendStartMenu(long userId)
    {
        var menu = new ReplyKeyboardMarkup(new[]
        {
            new KeyboardButton("Start Chatting"),
            new KeyboardButton("Stop Chatting")
        })
        {
            ResizeKeyboard = true
        };

        await _botClient.SendTextMessageAsync(userId, "Welcome to AnonChatBot! Choose an option:", replyMarkup: menu);
    }

    private static async Task StartChatting(long userId)
    {
        if (_activeChats.ContainsKey(userId))
        {
            await _botClient.SendTextMessageAsync(userId, "You are already in a chat.");
            return;
        }

        if (_waitingUsers.Count > 0)
        {
            var pairedUserId = _waitingUsers.Dequeue();
            _activeChats[userId] = pairedUserId;
            _activeChats[pairedUserId] = userId;

            await _botClient.SendTextMessageAsync(userId, "You are now connected to a stranger.");
            await _botClient.SendTextMessageAsync(pairedUserId, "You are now connected to a stranger.");
        }
        else
        {
            _waitingUsers.Enqueue(userId);
            await _botClient.SendTextMessageAsync(userId, "Looking for a stranger to chat with...");
        }
    }

    private static async Task StopChatting(long userId)
    {
        if (_activeChats.TryGetValue(userId, out var pairedUserId))
        {
            _activeChats.Remove(userId);
            _activeChats.Remove(pairedUserId);

            await _botClient.SendTextMessageAsync(userId, "You have left the chat.");
            await _botClient.SendTextMessageAsync(pairedUserId, "Your partner has left the chat.");
        }
        else
        {
            await _botClient.SendTextMessageAsync(userId, "You are not in a chat.");
        }
    }

    private static async Task RelayMessage(long userId, string text)
    {
        if (_activeChats.TryGetValue(userId, out var pairedUserId))
        {
            await _botClient.SendTextMessageAsync(pairedUserId, text);
        }
        else
        {
            await _botClient.SendTextMessageAsync(userId, "You are not in a chat. Type /start to begin.");
        }
    }
}

//public class UserState
//{
//    public bool IsWaiting { get; set; }
//    public long? PairedUserId { get; set; }
//}