using System.Threading;
using System.Threading.Tasks;
using LittleThingsToDo.TelegramBot.Services;
using MediatR;
using Telegram.Bot.Types.ReplyMarkups;

namespace LittleThingsToDo.TelegramBot.LittleThing.AddLittleThingMenu
{
    public class AddLittleThingMenuCommandHandler : CommandHandlerBase, IRequestHandler<AddLittleThingMenuCommand>
    {
        protected readonly ICurrentChatService _currentChatService;

        public AddLittleThingMenuCommandHandler(IBotClient botClient,
            ICurrentChatService currentChatService) 
            : base(botClient)
        {
            _currentChatService = currentChatService;
        }

        public async Task<Unit> Handle(AddLittleThingMenuCommand request, CancellationToken cancellationToken)
        {
            await _botClient.Client.SendTextMessageAsync(_currentChatService.CurrentChatId,
                "Please enter new little thing. You can actually enter a list, just use a comma.",
                replyMarkup: new ForceReplyMarkup(),
                cancellationToken: cancellationToken);

            return default;
        }
    }
}
