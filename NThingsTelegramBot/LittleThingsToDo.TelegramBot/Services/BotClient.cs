using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace LittleThingsToDo.TelegramBot.Services
{
    public class BotClient : IBotClient
    {
        public BotClient(IOptions<BotConfiguration> options)
        {
            Client = new TelegramBotClient(options.Value.Token);
        }

        public TelegramBotClient Client { get; }
    }
}