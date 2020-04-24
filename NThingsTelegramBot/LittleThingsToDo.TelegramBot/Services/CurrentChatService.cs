using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LittleThingsToDo.TelegramBot.Services
{
    public class CurrentChatService : ICurrentChatService
    {
        private ChatId _currentChatId;

        public ChatId CurrentChatId => _currentChatId;

        public void SetFromUpdate(Update update)
            => _currentChatId = GetMessage(update).Chat.Id;

        private Message GetMessage(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    return update.Message;
                case UpdateType.CallbackQuery:
                    return update.CallbackQuery.Message;
                case UpdateType.EditedMessage:
                    return update.EditedMessage;
                case UpdateType.ChannelPost:
                    return update.ChannelPost;
                case UpdateType.EditedChannelPost:
                    return update.EditedChannelPost;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}