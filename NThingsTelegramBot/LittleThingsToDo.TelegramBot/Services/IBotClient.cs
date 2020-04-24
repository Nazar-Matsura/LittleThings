using Telegram.Bot;

namespace LittleThingsToDo.TelegramBot.Services
{
    public interface IBotClient
    {
        TelegramBotClient Client { get; }
    }
}