using Telegram.Bot;

namespace NThingsTelegramBot.Services
{
    public interface IBotClient
    {
        TelegramBotClient Client { get; }
    }
}