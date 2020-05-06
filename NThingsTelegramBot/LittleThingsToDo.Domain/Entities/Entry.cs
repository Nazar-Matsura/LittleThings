using System;
using LittleThingsToDo.Domain.Common;

namespace LittleThingsToDo.Domain.Entities
{
    public class Entry : BaseEntity
    {
        protected Entry()
        {
        }

        public Entry(LittleThing littleThing)
        {
            LittleThing = littleThing;
            LittleThingId = littleThing.Id;
        }

        public Guid LittleThingId { get; private set; }

        public virtual LittleThing LittleThing { get; private set; }

        public static Spec<Entry> CreatedTodaySpec
        {
            get{return new Spec<Entry>(e => e.CreatedOn.Date == DateTime.Today);}
        }
    }
}