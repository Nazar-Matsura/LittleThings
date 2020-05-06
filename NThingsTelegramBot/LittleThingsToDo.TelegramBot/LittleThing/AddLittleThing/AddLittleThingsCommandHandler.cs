using System.Linq;
using System.Text;
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
            ICurrentChatService currentChatService,
            ILittleThingService littleThingService) 
            : base(botClient, currentChatService)
        {
            _littleThingService = littleThingService;
        }

        public async Task<Unit> Handle(AddLittleThingCommand request, CancellationToken cancellationToken)
        {
            var names = request.Update.Message.Text
                .Split(", ")
                .ToList();

            StringBuilder resultText = new StringBuilder();
            if(names.Any())
            {
                await _littleThingService.AddList(names);
                resultText.Append("Added new little thing");
                if (names.Count > 1)
                    resultText.Append("s");
            }
            else
            {
                resultText.Append("No new lt's were added");
            }

            await ReplyText(resultText.ToString());

            return default;
        }
    }
}