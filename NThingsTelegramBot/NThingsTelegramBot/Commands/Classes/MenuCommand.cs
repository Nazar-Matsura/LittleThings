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

        public MenuCommand(IBotClient botClient,
            ILittleThingService littleThingService) : base(botClient)
        {
            _littleThingService = littleThingService;
        }

        public override async Task Handle(Update update)
        {
            var buttons = BuildMenuButtons();
            var markup = new InlineKeyboardMarkup(buttons);
            
            await _botClient.Client.SendTextMessageAsync(update.Message.Chat.Id, "Main Menu", replyMarkup:buttons);
        }

        private IEnumerable<IEnumerable<InlineKeyboardButton>> BuildMenuButtons()
        {
            var littleThings = _littleThingService
                .GetLittleThings();

            var result = new List<InlineKeyboardButton>();

            foreach (var littleThing in littleThings)
            {
                result.Add(new InlineKeyboardButton {CallbackData = littleThing.Id.ToString()});
            }

            result.Add(new InlineKeyboardButton {Text = "[Remove]", CallbackData = Guid.Empty.ToString()});

            return new List<IEnumerable<InlineKeyboardButton>> {result};
        }
    }
}
