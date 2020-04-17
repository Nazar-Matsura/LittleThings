using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.Commands.Interfaces;
using LittleThingsToDo.TelegramBot.Common;
using LittleThingsToDo.TelegramBot.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace LittleThingsToDo.TelegramBot.Commands.Classes
{
    public class MenuCommand : Command, IMenuCommand
    {
        private readonly ILittleThingService _littleThingService;
        private readonly ICallbackDataFormatter _callbackDataFormatter;
        private readonly ICurrentChatService _currentChatService;

        public MenuCommand(IBotClient botClient,
            ILittleThingService littleThingService, 
            ICallbackDataFormatter callbackDataFormatter,
            ICurrentChatService currentChatService) : base(botClient)
        {
            _littleThingService = littleThingService;
            _callbackDataFormatter = callbackDataFormatter;
            _currentChatService = currentChatService;
        }

        public override async Task Handle(Update update)
        {
            var buttons = await BuildMenuButtons();
            var markup = new InlineKeyboardMarkup(buttons);
            
            await _botClient.Client.SendTextMessageAsync(_currentChatService.CurrentChatId, "Little Things", replyMarkup:markup);
        }

        private async Task<IEnumerable<IEnumerable<InlineKeyboardButton>>> BuildMenuButtons()
        {
            var littleThings = await _littleThingService
                .GetLittleThings();

            var result = new List<InlineKeyboardButton>();
            
            var callbackData = new CallbackData<Guid>(CallbackCommand.AddEntry, Guid.Empty);
            foreach (var littleThing in littleThings)
            {
                callbackData.Data = littleThing.Id; // Optimization? We limit number of allocations to one, but sacrifice encapsulation.
                result.Add(new InlineKeyboardButton {Text = littleThing.Name, CallbackData = callbackData.ToJson()});
            }

            callbackData.Command = 
            result.Add(new InlineKeyboardButton {Text = "[Add New]", CallbackData = });

            return new List<IEnumerable<InlineKeyboardButton>> {result};
        }
    }
}
