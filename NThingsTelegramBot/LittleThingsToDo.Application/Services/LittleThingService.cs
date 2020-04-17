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
        private readonly ICurrentAuthorService _currentAuthor;
        
        public LittleThingService(IRepository<LittleThing> littleThingsRepository,
            ICurrentAuthorService currentAuthor)
        {
            _littleThingsRepository = littleThingsRepository;
            _currentAuthor = currentAuthor;
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
            throw new NotImplementedException();
        }

        public async Task AddEntry()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Entry>> GetEntriesForToday()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LittleThing>> GetLittleThings()
        {
            var currentAuthorId = _currentAuthor.CurrentAuthorId;
            var littleThings = await _littleThingsRepository.GetAll(BaseEntity.CreatedBySpec<LittleThing>(currentAuthorId));
            return littleThings.ToList();
        }
    }
}