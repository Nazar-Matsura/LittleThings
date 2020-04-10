using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace NThingsTelegramBot.Services
{
    public class TempService : ITempService
    {
        private readonly IBotClient _botClient;

        public TempService(IBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task SayHello(Message message)
        {
            await _botClient.Client.SendTextMessageAsync(message.Chat.Id, "Sup bro");
        }
    }
}