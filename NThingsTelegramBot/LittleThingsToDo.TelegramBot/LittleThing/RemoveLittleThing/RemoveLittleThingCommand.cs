using System;
using MediatR;

namespace LittleThingsToDo.TelegramBot.LittleThing.RemoveLittleThing
{
    public class RemoveLittleThingCommand : IRequest
    {
        public RemoveLittleThingCommand(Guid littleThingId)
        {
            LittleThingId = littleThingId;
        }

        public Guid LittleThingId { get; }
    }
}
