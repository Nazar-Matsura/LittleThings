using System.Threading.Tasks;
using LittleThingsToDo.TelegramBot.Commands.Interfaces;
using LittleThingsToDo.TelegramBot.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace LittleThingsToDo.TelegramBot.Commands.Classes
{
    public class AddLittleThingMenuCommand : Command, IAddLittleThingMenuCommand
    {
        private readonly ICurrentChatService _currentChatService;
        
        public AddLittleThingMenuCommand(
            IBotClient botClient, 
            ICurrentChatService currentChatService) : base(botClient)
        {
            _currentChatService = currentChatService;
        }

        public override async Task Handle(Update update)
        {
            var forceReply = new ForceReplyMarkup();
            await _botClient.Client.SendTextMessageAsync(_currentChatService.CurrentChatId.Identifier,
                "Enter new little thing name. You can add a few, actually, just use a comma",
                replyMarkup: forceReply);
        }

    }
}