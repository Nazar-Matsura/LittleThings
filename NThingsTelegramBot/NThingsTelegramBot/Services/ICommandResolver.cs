using MediatR;
using Telegram.Bot.Types;

namespace LittleThingsToDo.TelegramBot.Services
{
    public interface ICommandResolver
    {
        IRequest Resolve(Update update);
    }
}