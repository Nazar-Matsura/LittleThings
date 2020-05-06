using System.Threading;
using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.Services;
using MediatR;

namespace LittleThingsToDo.TelegramBot.LittleThing.RemoveLittleThing
{
    public class RemoveLittleThingCommandHandler : CommandHandlerBase, IRequestHandler<RemoveLittleThingCommand, Unit>
    {
        private readonly ILittleThingService _littleThingService;

        public RemoveLittleThingCommandHandler(IBotClient botClient,
            ICurrentChatService currentChatService,
            ILittleThingService littleThingService) 
            : base(botClient, currentChatService)
        {
            _littleThingService = littleThingService;
        }

        public async Task<Unit> Handle(RemoveLittleThingCommand request, CancellationToken cancellationToken)
        {
            await _littleThingService.Remove(request.LittleThingId);
            await ReplyText("removed lt");

            return default;
        }
    }
}