using System.Linq;
using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.Commands.Interfaces;
using LittleThingsToDo.TelegramBot.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace LittleThingsToDo.TelegramBot.Commands.Classes
{
    public class AddLittleThingMenuCommand : Command, IAddLittleThingMenuCommand
    {
        public override async Task Handle(Update update)
        {
            var forceReply = new ForceReplyMarkup();
            await _botClient.Client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id,
                "Enter new little thing name. You can add a few, actually, just use a comma",
                replyMarkup: forceReply);
        }

        public AddLittleThingMenuCommand(IBotClient botClient) : base(botClient)
        {
        }
    }
}