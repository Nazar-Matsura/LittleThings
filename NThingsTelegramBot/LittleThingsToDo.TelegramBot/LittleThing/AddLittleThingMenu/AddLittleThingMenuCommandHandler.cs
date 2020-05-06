using System.Threading;
using System.Threading.Tasks;
using LittleThingsToDo.TelegramBot.LittleThing.AddLittleThing;
using LittleThingsToDo.TelegramBot.Services;
using LittleThingsToDo.TelegramBot.Storage;
using MediatR;
using Telegram.Bot.Types.ReplyMarkups;

namespace LittleThingsToDo.TelegramBot.LittleThing.AddLittleThingMenu
{
    public class AddLittleThingMenuCommandHandler : CommandHandlerBase, IRequestHandler<AddLittleThingMenuCommand>
    {
        protected readonly ICommandStorage _commandStorage;

        public AddLittleThingMenuCommandHandler(IBotClient botClient,
            ICurrentChatService currentChatService,
            ICommandStorage commandStorage)
            : base(botClient, currentChatService)
        {
            _commandStorage = commandStorage;
        }

        public async Task<Unit> Handle(AddLittleThingMenuCommand request, CancellationToken cancellationToken)
        {
            _commandStorage.AddOrUpdate(_currentChatService.CurrentChatId, new AddLittleThingCommand());

            await _botClient.Client.SendTextMessageAsync(_currentChatService.CurrentChatId,
                "Please enter new little thing. You can actually enter a list, just use a comma.",
                replyMarkup: new ForceReplyMarkup(),
                cancellationToken: cancellationToken);

            return default;
        }
    }
}
