using System.Threading;
using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.Services;
using MediatR;

namespace LittleThingsToDo.TelegramBot.LittleThing.AddLittleThingEntry
{
    public class AddLittleThingEntryCommandHandler : CommandHandlerBase, IRequestHandler<AddLittleThingEntryCommand>
    {
        protected readonly ILittleThingService _littleThingService;

        public AddLittleThingEntryCommandHandler(IBotClient botClient,
            ILittleThingService littleThingService) : base(botClient)
        {
            _littleThingService = littleThingService;
        }

        public async Task<Unit> Handle(AddLittleThingEntryCommand request, CancellationToken cancellationToken)
        {
            await _littleThingService.AddEntry(request.LittleThingId);

            return default;
        }
    }
}
