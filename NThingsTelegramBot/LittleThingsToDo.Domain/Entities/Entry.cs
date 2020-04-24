using System;

namespace LittleThingsToDo.Domain.Entities
{
    public class Entry : BaseEntity
    {
        private Entry(Guid littleThingId)
        {
            LittleThingId = littleThingId;
        }

        public Entry(LittleThing littleThing)
            :this(littleThing.Id)
        {
            LittleThing = littleThing;
        }

        public Guid LittleThingId { get; private set; }

        public LittleThing LittleThing { get; private set; }
    }
}