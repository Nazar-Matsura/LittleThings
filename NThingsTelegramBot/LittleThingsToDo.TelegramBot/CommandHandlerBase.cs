using System.Threading.Tasks;
using LittleThingsToDo.TelegramBot.Services;

namespace LittleThingsToDo.TelegramBot
{
    public abstract class CommandHandlerBase
    {
        protected readonly IBotClient _botClient;

        protected readonly ICurrentChatService _currentChatService;

        // ReSharper disable once PublicConstructorInAbstractClass
        // If constructor is protected, ReSharper automatically generated protected c-tors in derived classes, which is irritating
        public CommandHandlerBase(IBotClient botClient, ICurrentChatService currentChatService)
        {
            _botClient = botClient;
            _currentChatService = currentChatService;
        }

        protected virtual async Task ReplyText(string text)
        {
            await _botClient.Client.SendTextMessageAsync(_currentChatService.CurrentChatId, text);
        }
    }
}
