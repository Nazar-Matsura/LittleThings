using MediatR;
using Telegram.Bot.Types;

namespace LittleThingsToDo.TelegramBot
{
    public interface IForceReplyCommand : IRequest
    {
        Update Update { get; }
    }
}