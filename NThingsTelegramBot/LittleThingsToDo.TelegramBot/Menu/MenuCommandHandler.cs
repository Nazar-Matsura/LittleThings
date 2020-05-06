using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.LittleThing.AddLittleThingMenu;
using LittleThingsToDo.TelegramBot.LittleThing.GetEntriesForToday;
using LittleThingsToDo.TelegramBot.LittleThing.LittleThingSelectedMenu;
using LittleThingsToDo.TelegramBot.Services;
using MediatR;
using Telegram.Bot.Types.ReplyMarkups;

namespace LittleThingsToDo.TelegramBot.Menu
{
    public class MenuCommandHandler : CommandHandlerBase, IRequestHandler<MenuCommand>
    {
        private readonly ILittleThingService _littleThingService;
        private readonly ICallbackDataFormatter _callbackDataFormatter;

        public MenuCommandHandler(IBotClient botClient,
            ILittleThingService littleThingService, 
            ICallbackDataFormatter callbackDataFormatter,
            ICurrentChatService currentChatService)
            : base(botClient, currentChatService)
        {
            _littleThingService = littleThingService;
            _callbackDataFormatter = callbackDataFormatter;
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

            var result = new List<List<InlineKeyboardButton>>();
            
            foreach (var littleThing in littleThings)
            {
                var addEntry = new LittleThingSelectedMenuCommand(littleThing.Id);
                AddButton(littleThing.Name, addEntry, result);
            }
            
            AddLittleThingMenuButton(result);

            PrintTodayProgressButton(result);
            
            return result;
        }

        private void PrintTodayProgressButton(List<List<InlineKeyboardButton>> result)
        {
            var printProgress = new GetEntriesForTodayCommand();
            AddButton("[Today's Progress]", printProgress, result);

        }

        private void AddLittleThingMenuButton(List<List<InlineKeyboardButton>> result)
        {
            var addLittleThingMenu = new AddLittleThingMenuCommand();
            AddButton("[Add New]", addLittleThingMenu, result);
        }

        private void AddButton(string text, IRequest command, List<List<InlineKeyboardButton>> list)
        {
            var button = new InlineKeyboardButton
                {Text = text, CallbackData = _callbackDataFormatter.ToString(command)};
            list.Add(new List<InlineKeyboardButton>{button});
        }
    }
}
