using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace LittleThingsToDo.TelegramBot.Services
{
    public interface ITempService
    {
        Task SayHello(Message message);
    }
}