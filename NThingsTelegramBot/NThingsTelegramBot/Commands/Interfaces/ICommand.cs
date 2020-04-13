using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace LittleThingsToDo.TelegramBot.Commands.Interfaces
{
    public interface ICommand
    {
        Task Handle(Update update);
    }
}