using Telegram.Bot.Types;

namespace LittleThingsToDo.TelegramBot.LittleThing.AddLittleThing
{
    public class AddLittleThingCommand : IForceReplyCommand
    {
        public AddLittleThingCommand()
        {
        }

        public AddLittleThingCommand(Update update)
        {
            Update = update;
        }

        public Update Update { get; set; }
    }
}
