using System.Threading.Tasks;
using LittleThingsToDo.TelegramBot.Commands.Interfaces;
using LittleThingsToDo.TelegramBot.Services;
using Telegram.Bot.Types;

namespace LittleThingsToDo.TelegramBot.Commands.Classes
{
    public abstract class Command : ICommand
    {
        protected readonly IBotClient _botClient;

        protected Command(IBotClient botClient) => _botClient = botClient;

        public abstract Task Handle(Update update);

        protected override 
    }
}