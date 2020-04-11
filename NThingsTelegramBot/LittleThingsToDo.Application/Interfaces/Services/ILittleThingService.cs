using System;
using System.Collections.Generic;
using LittleThingsToDo.Domain.Entities;

namespace LittleThingsToDo.Application.Interfaces.Services
{
    public interface ILittleThingService
    {
        void Add();
        void Remove(Guid id);
        void AddEntry();
        List<Entry> GetEntriesForToday();
    }
}