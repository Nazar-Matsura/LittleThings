using MediatR;
using Telegram.Bot.Types;

namespace LittleThingsToDo.TelegramBot
{
    public interface IForceReplyCommand : IRequest<Unit>
    {
        Update Update { get; }
    }
}