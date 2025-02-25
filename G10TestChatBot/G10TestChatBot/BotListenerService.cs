using ChatBot.Bll.Services;
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
    private readonly IBotUserService botUserService;
    private readonly IUserInfoService userInfoService;
    private readonly IEducationService educationService;
    private readonly ISkillService skillService;
    private readonly IExperienceService experienceService;
    private readonly IFileService fileService;

    public BotListenerService(IBotUserService userService, IUserInfoService userInfoService, IEducationService educationService, ISkillService skillService, IExperienceService experienceService, IFileService fileService)
    {
        this.botUserService = userService;
        this.userInfoService = userInfoService;
        this.educationService = educationService;
        this.skillService = skillService;
        this.experienceService = experienceService;
        this.fileService = fileService;
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
            var botUserId = await botUserService.GetBotUserIdByTelegramUserIdAsync(user.Id);

            if (message.Text == "/start")
            {
                var savingUser = new BotUser()
                {
                    TelegramUserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    UpdatedAt = DateTime.UtcNow,
                };

                await botUserService.AddUserAsync(savingUser);

                await SendStartMenu(bot, user.Id);
                return;
            }

            if(message.Text == "Main menu")
            {
                var menu = new ReplyKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new KeyboardButton("User Info"),
                        new KeyboardButton("Education"),
                        new KeyboardButton("Experience")
                    },
                    new[]
                    {
                        new KeyboardButton("Skills"),
                        new KeyboardButton("Get CV in .pdf"),
                        new KeyboardButton("Get CV in .word")
                    }
                })
                {
                     ResizeKeyboard = true
                };

                await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "You get main menu",
                    parseMode: ParseMode.Markdown,
                    replyMarkup: menu
                );

                return;
            }

            if (message.Text == "User Info")
            {
                var userInfo = await userInfoService.GetUserInfoByTelegramIdAsync(botUserId);

                var menu = new ReplyKeyboardMarkup()
                {
                    ResizeKeyboard = true
                };

                var textOfUserInfo = "";

                if (userInfo == null)
                {
                    textOfUserInfo = "Your info not found press\nCreate user info button to create";

                    menu.AddButtons(
                        new KeyboardButton("Create user info"),
                        new KeyboardButton("Main menu"));
                }
                else
                {
                    textOfUserInfo = $"Your personal info below\n" +
                                     $"UserId      : {userInfo.UserInfoId}\n" +
                                     $"Firstname   : {userInfo.FirstName}\n" +
                                     $"Lastname    : {userInfo.LastName}\n" +
                                     $"Phonenumber : {userInfo.PhoneNumber}\n" +
                                     $"Email       : {userInfo.Email}\n" +
                                     $"Address     : {userInfo.Address}\n" +
                                     $"Summary     : {userInfo.Summary}\n";

                    menu.AddButtons(
                        new KeyboardButton("Update user info"),
                        new KeyboardButton("Delete all user info"),
                        new KeyboardButton("Main menu"));
                }

                await bot.SendTextMessageAsync(
                chatId: user.Id,
                text: textOfUserInfo,
                parseMode: ParseMode.Markdown,
                replyMarkup: menu
                );

                return;
            }

            if (message.Text == "Create user info")
            {
                var userInfoText = "Please enter your details in the following format:\n\n" +
                      "*First Name*\n" +
                      "*Last Name*\n" +
                      "*Email*\n" +
                      "*Phone Number*\n" +
                      "*Address*\n" +
                      "*Summary*\n\n" +
                      "Example:\n" +
                      "John\n" +
                      "Doe\n" +
                      "john.doe@example.com\n" +
                      "+1234567890\n" +
                      "123 Main St, City, Country\n" +
                      "I am .net developer";

                await bot.SendTextMessageAsync(
                chatId: user.Id,
                text: userInfoText,
                parseMode: ParseMode.Markdown
                );

                return;
            }

            if (message.Text.StartsWith("Create user info"))
            {
                var userInfotext = message.Text;
                var data = userInfotext.Split("\n");
                var userInfo = new UserInfo()
                {
                    FirstName = data[1].Trim(),
                    LastName = data[2].Trim(),
                    Email = data[3].Trim(),
                    PhoneNumber = data[4].Trim(),
                    Address = data[5].Trim(),
                    BotUserId = botUserId
                };

                for (var i = 6; i < data.Count(); i++)
                {
                    userInfo.Summary += data[i].Trim() + " ";
                }

                var resFromAddUserInfoAsync = await userInfoService.AddUserInfoAsync(userInfo);

                var textToBotUser = "";

                if (resFromAddUserInfoAsync == 0)
                {
                    textToBotUser = "Error occuried while saving";
                }
                else
                {
                    textToBotUser = "Successfully saved";
                }

                await bot.SendTextMessageAsync(
                chatId: user.Id,
                text: textToBotUser,
                parseMode: ParseMode.Markdown
                );

                await SendStartMenu(bot, user.Id);
            }

            if (message.Text == "Education")
            {

                return;
            }

            if (message.Text == "Experience")
            {
                return;
            }

            if (message.Text == "Skills")
            {
                return;
            }

            if (message.Text == "Get CV in .pdf")
            {
                var pdfBytes = await fileService.GenerateCVAsync(botUserId);

                using (var stream = new MemoryStream(pdfBytes))
                {
                    await botClient.SendDocumentAsync(
                        chatId: message.Chat.Id,
                        document: new InputFileStream(stream, "CV.pdf"),
                        caption: "Here is your CV in PDF format."
                    );
                }

                return;
            }


            if (message.Text == "Get CV in .word")
            {
                return;
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

    private static async Task SendUserInfoMenu(ITelegramBotClient botClient, long userId)
    {
        var menu = new ReplyKeyboardMarkup(
            new KeyboardButton("Fill user info"),
            new KeyboardButton("Update user info"),
            new KeyboardButton("Delete user info"))
        {
            ResizeKeyboard = true
        };


    }

    private static async Task SendStartMenu(ITelegramBotClient botClient, long userId)
    {
        var menu = new ReplyKeyboardMarkup(new[]
        {
            new[]
            {
                new KeyboardButton("User Info"),
                new KeyboardButton("Education"),
                new KeyboardButton("Experience")
            },
            new[]
            {
                new KeyboardButton("Skills"),
                new KeyboardButton("Get CV in .pdf"),
                new KeyboardButton("Get CV in .word")
            }
        })
        {
            ResizeKeyboard = true
        };



        var introText = @"
            🌟 *Welcome to the CV Builder Bot!* 🌟

            I'm here to help you create a **professional CV in PDF format** effortlessly. Here's how it works:

            1. **Provide Your Information**: Fill in your personal details, work experience, education, and skills.
            2. **Review and Confirm**: You can review and edit your information at any time.
            3. **Generate Your CV**: Once everything is ready, I'll create a polished PDF version of your CV for you to download.

            📝 *What you'll need to provide:*
            - **User Info**: Name, contact details, etc.
            - **Education**: Your academic background and qualifications.
            - **Experience**: Your past jobs, roles, and achievements.
            - **Skills**: Your key skills and expertise.

            🚀 *Ready to get started?*
            Use the buttons below to fill in your details or generate your CV!

            Need help? Just type /help at any time.

            Let's create an amazing CV together! 😊
            ";

        await botClient.SendTextMessageAsync(
            chatId: userId,
            text: introText,
            parseMode: ParseMode.Markdown,
            replyMarkup: menu
        );
    }
}
