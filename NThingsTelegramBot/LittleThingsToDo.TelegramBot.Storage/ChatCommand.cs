using System;

namespace TelegramBot.Storage
{
    public class ChatCommand
    {
        public ChatCommand(long chatId, string commandType, string data)
        {
            Id = Guid.NewGuid();
            ChatId = chatId;
            CommandType = commandType;
            Data = data;
            CreatedOn = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public long ChatId { get; private set; }
        public string CommandType { get; private set; }
        public string Data { get; private set; }
        public DateTime CreatedOn { get; private set; }
    }
}
