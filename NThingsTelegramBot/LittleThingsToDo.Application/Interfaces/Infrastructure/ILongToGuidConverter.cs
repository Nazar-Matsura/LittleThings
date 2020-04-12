using System;

namespace LittleThingsToDo.Application.Interfaces.Infrastructure
{
    public interface ILongToGuidConverter
    {
        Guid Convert(long val);
        long Convert(Guid val);
    }
}