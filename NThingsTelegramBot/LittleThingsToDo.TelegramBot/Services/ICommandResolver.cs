using System.Threading.Tasks;
using MediatR;
using Telegram.Bot.Types;

namespace LittleThingsToDo.TelegramBot.Services
{
    public interface ICommandResolver
    {
        Task<IRequest> Resolve(Update update);
    }
}