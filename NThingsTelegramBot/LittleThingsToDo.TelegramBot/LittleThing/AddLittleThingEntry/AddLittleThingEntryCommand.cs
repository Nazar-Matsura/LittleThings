using System;
using MediatR;

namespace LittleThingsToDo.TelegramBot.LittleThing.AddLittleThingEntry
{
    public class AddLittleThingEntryCommand : IRequest
    {
        public AddLittleThingEntryCommand(Guid littleThingId)
        {
            LittleThingId = littleThingId;
        }

        public Guid LittleThingId { get; }
    }
}
