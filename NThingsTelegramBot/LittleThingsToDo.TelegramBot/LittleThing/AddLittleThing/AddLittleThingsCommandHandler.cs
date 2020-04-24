using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.Services;
using MediatR;

namespace LittleThingsToDo.TelegramBot.LittleThing.AddLittleThing
{
    public class AddLittleThingsCommandHandler : CommandHandlerBase, IRequestHandler<AddLittleThingCommand, Unit>
    {
        private readonly ILittleThingService _littleThingService;
        
        public AddLittleThingsCommandHandler(IBotClient botClient, 
            ILittleThingService littleThingService) : base(botClient)
        {
            _littleThingService = littleThingService;
        }

        public async Task<Unit> Handle(AddLittleThingCommand request, CancellationToken cancellationToken)
        {
            var names = request.Update.Message.Text
                .Split(", ")
                .ToList();

            if(names.Any())
            {
                await _littleThingService.AddList(names);
            }

            return default;
        }
    }
}