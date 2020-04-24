using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LittleThingsToDo.Domain.Entities;

namespace LittleThingsToDo.Application.Interfaces.Services
{
    public interface ILittleThingService
    {
        Task AddList(List<string> names);
        Task Remove(Guid id);
        Task AddEntry(Guid id);
        Task<List<Entry>> GetEntriesForToday();
        Task<List<LittleThing>> GetLittleThings();
    }
}