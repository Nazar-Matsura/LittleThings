using Telegram.Bot.Types;

namespace LittleThingsToDo.TelegramBot.Services
{
    public interface ICurrentChatService
    {
        ChatId CurrentChatId { get; }

        void SetFromUpdate(Update update);
    }
}