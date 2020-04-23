using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.LittleThing.AddLittleThingEntry;
using LittleThingsToDo.TelegramBot.LittleThing.AddLittleThingMenu;
using LittleThingsToDo.TelegramBot.Services;
using MediatR;
using Telegram.Bot.Types.ReplyMarkups;

namespace LittleThingsToDo.TelegramBot.Menu
{
    public class MenuCommandHandler : CommandHandlerBase, IRequestHandler<MenuCommand>
    {
        private readonly ILittleThingService _littleThingService;
        private readonly ICallbackDataFormatter _callbackDataFormatter;
        private readonly ICurrentChatService _currentChatService;

        public MenuCommandHandler(IBotClient botClient,
            ILittleThingService littleThingService, 
            ICallbackDataFormatter callbackDataFormatter,
            ICurrentChatService currentChatService) : base(botClient)
        {
            _littleThingService = littleThingService;
            _callbackDataFormatter = callbackDataFormatter;
            _currentChatService = currentChatService;
        }

        public async Task<Unit> Handle(MenuCommand request, CancellationToken cancellationToken)
        {
            var buttons = await BuildMenuButtons();
            var markup = new InlineKeyboardMarkup(buttons);

            await _botClient.Client.SendTextMessageAsync(_currentChatService.CurrentChatId, "Little Things",
                replyMarkup: markup, cancellationToken: cancellationToken);

            return default;
        }

        private async Task<IEnumerable<IEnumerable<InlineKeyboardButton>>> BuildMenuButtons()
        {
            var littleThings = await _littleThingService
                .GetLittleThings();

            var result = new List<InlineKeyboardButton>();
            
            foreach (var littleThing in littleThings)
            {
                var addEntry = new AddLittleThingEntryCommand(littleThing.Id);
                result.Add(new InlineKeyboardButton {Text = littleThing.Name, CallbackData = _callbackDataFormatter.ToString(addEntry)});
            }

            var addLittleThingMenu = new AddLittleThingMenuCommand();
            result.Add(new InlineKeyboardButton {Text = "[Add New]", CallbackData = _callbackDataFormatter.ToString(addLittleThingMenu)});

            return new List<IEnumerable<InlineKeyboardButton>> {result};
        }
    }
}
