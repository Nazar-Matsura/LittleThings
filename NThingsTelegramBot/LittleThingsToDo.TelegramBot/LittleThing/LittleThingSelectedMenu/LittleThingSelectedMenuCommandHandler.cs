using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LittleThingsToDo.TelegramBot.LittleThing.AddLittleThingEntry;
using LittleThingsToDo.TelegramBot.LittleThing.RemoveLittleThing;
using LittleThingsToDo.TelegramBot.Services;
using MediatR;
using Telegram.Bot.Types.ReplyMarkups;

namespace LittleThingsToDo.TelegramBot.LittleThing.LittleThingSelectedMenu
{
    public class LittleThingSelectedMenuCommandHandler : CommandHandlerBase, IRequestHandler<LittleThingSelectedMenuCommand>
    {
        private readonly ICallbackDataFormatter _callbackDataFormatter;

        public LittleThingSelectedMenuCommandHandler(IBotClient botClient,
            ICurrentChatService currentChatService,
            ICallbackDataFormatter callbackDataFormatter) 
            : base(botClient, currentChatService)
        {
            _callbackDataFormatter = callbackDataFormatter;
        }

        public async Task<Unit> Handle(LittleThingSelectedMenuCommand request, CancellationToken cancellationToken)
        {
            var buttons = BuildButtons(request.LittleThingId);

            var menu = new InlineKeyboardMarkup(buttons);

            await _botClient.Client.SendTextMessageAsync(_currentChatService.CurrentChatId, "What do?",
                replyMarkup: menu, cancellationToken: cancellationToken);

            return default;
        }

        private IEnumerable<InlineKeyboardButton> BuildButtons(Guid requestLittleThingId)
        {
            var addEntry = new AddLittleThingEntryCommand(requestLittleThingId);

            var removeLittleThing = new RemoveLittleThingCommand(requestLittleThingId);

            return new[]
            {
                new InlineKeyboardButton {CallbackData = _callbackDataFormatter.ToString(addEntry), Text = "Did it"},
                new InlineKeyboardButton {CallbackData = _callbackDataFormatter.ToString(removeLittleThing), Text = "[Remove]"},
            };
        }
    }
}
