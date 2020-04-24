using MediatR;

namespace LittleThingsToDo.TelegramBot.LittleThing.AddLittleThing
{
    public class AddLittleThingCommand : IRequest
    {
        public string LittleThingsList { get; set; }
    }
}
