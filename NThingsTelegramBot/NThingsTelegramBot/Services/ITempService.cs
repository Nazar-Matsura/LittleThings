using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace NThingsTelegramBot.Services
{
    public interface ITempService
    {
        Task SayHello(Message message);
    }
}