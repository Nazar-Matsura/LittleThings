using System;
using System.Threading.Tasks;
using LittleThingsToDo.Infrastructure.Extensions;
using LittleThingsToDo.TelegramBot.Storage;
using MediatR;
using Telegram.Bot.Types;

namespace LittleThingsToDo.TelegramBot
{
    public static class CommandStorageExtension
    {
        public static void AddOrUpdate(this ICommandStorage storage, ChatId chatId, IRequest command)
        {
            var commandType = command.GetType();
            
            storage.AddOrUpdate(new ChatCommand(chatId.Identifier, commandType.ToString()));
        }

        public static async Task<Type> GetLastCommandOfChatAsync(this ICommandStorage storage, ChatId chatId)
        {
            var lastCommand = await storage.GetLastCommandOfChatAsync(chatId.Identifier);
            if (lastCommand == null)
                return null;

            var commandType = Type.GetType(lastCommand.CommandType);
            if(commandType == null || !commandType.Implements<IRequest>())
                throw new CommandTypeIsNotARequestException(commandType?.ToString());

            return commandType;
        }

        public static async Task CompleteAsync(this ICommandStorage storage, IRequest request, ChatId chatId)
        {
            var command = await storage.GetLastCommandOfChatAsync(chatId.Identifier);
            if (command != null && command.CommandType == request.GetType().ToString())
                await storage.CompleteAsync(command);
        }

        public class CommandTypeIsNotARequestException : Exception
        {
            public CommandTypeIsNotARequestException(string message) : base(message)
            {
            }
        }
    }
}