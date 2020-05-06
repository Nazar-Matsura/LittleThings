using System;
using MediatR;

namespace LittleThingsToDo.TelegramBot.LittleThing.LittleThingSelectedMenu
{
    public class LittleThingSelectedMenuCommand : IRequest
    {
        public LittleThingSelectedMenuCommand(Guid littleThingId)
        {
            LittleThingId = littleThingId;
        }

        public Guid LittleThingId { get; }
    }
}
