using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.Commands.Interfaces;
using LittleThingsToDo.TelegramBot.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace LittleThingsToDo.TelegramBot.Commands.Classes
{
    public class MenuCommand : Command, IMenuCommand
    {
        private readonly ILittleThingService _littleThingService;
        private readonly ICommandConstantsService _commandConstantsService;

        public MenuCommand(IBotClient botClient,
            ILittleThingService littleThingService, 
            ICommandConstantsService commandConstantsService) : base(botClient)
        {
            _littleThingService = littleThingService;
            _commandConstantsService = commandConstantsService;
        }

        public override async Task Handle(Update update)
        {
            var buttons = await BuildMenuButtons();
            var markup = new InlineKeyboardMarkup(buttons);
            
            await _botClient.Client.SendTextMessageAsync(update.Message.Chat.Id, "Little Things", replyMarkup:markup);
        }

        private async Task<IEnumerable<IEnumerable<InlineKeyboardButton>>> BuildMenuButtons()
        {
            var littleThings = await _littleThingService
                .GetLittleThings();

            var result = new List<InlineKeyboardButton>();

            foreach (var littleThing in littleThings)
            {
                result.Add(new InlineKeyboardButton {CallbackData = littleThing.Id.ToString()});
            }

            result.Add(new InlineKeyboardButton {Text = "[Add New]", CallbackData = _commandConstantsService.AddNewIdentifier});

            return new List<IEnumerable<InlineKeyboardButton>> {result};
        }
    }
}
