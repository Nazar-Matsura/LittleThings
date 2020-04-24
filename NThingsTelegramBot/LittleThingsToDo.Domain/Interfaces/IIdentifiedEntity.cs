using System;

namespace LittleThingsToDo.Domain.Interfaces
{
    public interface IIdentifiedEntity
    {
        Guid Id { get; }
    }
}