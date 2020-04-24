using LittleThingsToDo.TelegramBot.Services;

namespace LittleThingsToDo.TelegramBot
{
    public class CommandHandlerBase
    {
        protected readonly IBotClient _botClient;

        protected CommandHandlerBase(IBotClient botClient) => _botClient = botClient;
    }
}
