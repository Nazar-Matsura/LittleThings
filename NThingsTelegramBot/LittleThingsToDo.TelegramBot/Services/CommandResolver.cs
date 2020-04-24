using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LittleThingsToDo.Infrastructure.Extensions;
using LittleThingsToDo.TelegramBot.Menu;
using LittleThingsToDo.TelegramBot.Storage;
using MediatR;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LittleThingsToDo.TelegramBot.Services
{
    public class CommandResolver : ICommandResolver
    {
        protected readonly ICallbackDataFormatter _callbackDataFormatter;
        protected readonly ICurrentChatService _currentChatService;
        protected readonly ICommandStorage _commandStorage;
        private Update _update;

        public CommandResolver(ICallbackDataFormatter callbackDataFormatter,
            ICurrentChatService currentChatService,
            ICommandStorage commandStorage)
        {
            _callbackDataFormatter = callbackDataFormatter;
            _currentChatService = currentChatService;
            _commandStorage = commandStorage;
        }

        // TODO: rewrite with chain of responsibility
        public async Task<IRequest> Resolve(Update update)
        {
            _update = update;
            switch (update.Type)
            {
                case UpdateType.Message:
                    return await ResolveFromMessage();
                case UpdateType.CallbackQuery:
                    return ResolveFromCallbackQuery();
                default:
                    return Default();
            }
        }

        private IRequest ResolveFromCallbackQuery()
        {
            return _callbackDataFormatter.FromString(_update.CallbackQuery.Data);
        }

        private async Task<IRequest> ResolveFromMessage()
        {
            var lastCommandType = await _commandStorage.GetLastCommandOfChatAsync(_currentChatService.CurrentChatId);
            if (lastCommandType == null)
                return Default();

            var @params = ParamsForType(lastCommandType);

            return (IRequest)Activator.CreateInstance(lastCommandType, @params);
        }

        private object?[]? ParamsForType(Type lastCommandType)
        {
            var result = new List<object>();

            if (lastCommandType.Implements<IForceReplyCommand>())
            {
                result.Add(_update);
            }

            return result.ToArray();
        }

        private IRequest Default()
        {
            return new MenuCommand();
        }
    }
}