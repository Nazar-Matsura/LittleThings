using System.Linq;
using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.Commands.Interfaces;
using LittleThingsToDo.TelegramBot.Services;
using Telegram.Bot.Types;

namespace LittleThingsToDo.TelegramBot.Commands.Classes
{
    public class AddLittleThingsCommand : Command, IAddLittleThingsCommand
    {
        private readonly ILittleThingService _littleThingService;
        
        public AddLittleThingsCommand(IBotClient botClient, 
            ILittleThingService littleThingService) : base(botClient)
        {
            _littleThingService = littleThingService;
        }

        public override async Task Handle(Update update)
        {
            var names = update.Message.Text
                .Split(", ")
                .ToList();

            if(names.Any())
            {
                await _littleThingService.AddList(names);
            }
        }
    }
}