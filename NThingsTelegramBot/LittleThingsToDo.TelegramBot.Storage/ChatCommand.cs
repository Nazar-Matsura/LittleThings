using System;

namespace LittleThingsToDo.TelegramBot.Storage
{
    public class ChatCommand
    {
        public ChatCommand(long id, string commandType)
        {
            Id = id;
            CommandType = commandType;
            CreatedOn = DateTime.Now;
        }

        public long Id { get; private set; }
        public string CommandType { get; private set; }
        public DateTime CreatedOn { get; private set; }
    }
}
