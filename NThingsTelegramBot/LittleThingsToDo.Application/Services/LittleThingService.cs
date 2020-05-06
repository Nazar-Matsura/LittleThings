using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Infrastructure;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.Domain.Entities;

namespace LittleThingsToDo.Application.Services
{
    public class LittleThingService : ILittleThingService
    {
        private readonly IRepository<LittleThing> _littleThingsRepository;
        private readonly IRepository<Entry> _entriesRepository;
        private readonly ICurrentAuthorService _currentAuthor;
        
        public LittleThingService(IRepository<LittleThing> littleThingsRepository,
            ICurrentAuthorService currentAuthor,
            IRepository<Entry> entriesRepository)
        {
            _littleThingsRepository = littleThingsRepository;
            _currentAuthor = currentAuthor;
            _entriesRepository = entriesRepository;
        }

        public async Task AddList(List<string> names)
        {
            var currentAuthorId = _currentAuthor.CurrentAuthorId;
            
            var littleThings = await _littleThingsRepository.GetAll(BaseEntity.CreatedBySpec<LittleThing>(currentAuthorId));
            var existingLittleThingNames = littleThings.Select(lt => lt.Name);

            var littleThingsToAdd = names
                .Except(existingLittleThingNames)
                .Select(name => new LittleThing(name));

            await _littleThingsRepository.AddRange(littleThingsToAdd);
        }

        public async Task Remove(Guid id)
        {
            var entry = await _littleThingsRepository.GetSingle(id);
            if (entry != null)
            {
                await _littleThingsRepository.Remove(entry);
            }
        }

        public async Task AddEntry(Guid littleThingId)
        {
            var littleThing = await _littleThingsRepository.GetSingle(littleThingId);

            await _entriesRepository.Add(new Entry(littleThing));
        }

        public async Task<List<Entry>> GetEntriesForToday()
        {
            var currentAuthorId = _currentAuthor.CurrentAuthorId;
            var entries = await _entriesRepository.GetAll(BaseEntity.CreatedBySpec<Entry>(currentAuthorId) &
                                                Entry.CreatedTodaySpec);

            return entries.ToList();
        }

        public async Task<List<LittleThing>> GetLittleThings()
        {
            var currentAuthorId = _currentAuthor.CurrentAuthorId;
            var littleThings = await _littleThingsRepository.GetAll(BaseEntity.CreatedBySpec<LittleThing>(currentAuthorId));
            return littleThings.ToList();
        }
    }
}