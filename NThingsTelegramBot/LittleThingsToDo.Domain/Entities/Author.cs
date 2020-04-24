using System;
using LittleThingsToDo.Domain.Interfaces;

namespace LittleThingsToDo.Domain.Entities
{
    public class Author : IIdentifiedEntity
    {
        public Author(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}