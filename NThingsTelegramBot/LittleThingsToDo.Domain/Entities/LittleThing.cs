using System.Collections.Generic;
using LittleThingsToDo.Domain.Exceptions;
using LittleThingsToDo.Domain.Interfaces;

namespace LittleThingsToDo.Domain.Entities
{
    public class LittleThing : BaseEntity, INamed
    {
        public LittleThing(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new LittleThingInvalidNameException();
            }

            Name = name;

            Entries = new List<Entry>();
        }

        public string Name { get; private set; }

        public virtual ICollection<Entry> Entries { get; private set; }
    }
}