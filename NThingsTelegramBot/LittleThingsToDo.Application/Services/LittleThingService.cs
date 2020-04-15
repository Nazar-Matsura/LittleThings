using System;
using System.Collections.Generic;
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
        private readonly IAuthorService _authorService;

        public LittleThingService(IRepository<LittleThing> littleThingsRepository,
            ICurrentAuthorService currentAuthor,
            IAuthorService authorService)
        {
            _littleThingsRepository = littleThingsRepository;
            _currentAuthor = currentAuthor;
            _authorService = authorService;
        }

        public async Task AddList(List<string> names)
        {
            var currentAuthorId = _currentAuthor.CurrentAuthorId;
            await VerifyAuthorExists(currentAuthorId);

            var littleThings = _littleThingsRepository.GetAll(BaseEntity.CreatedBySpec(currentAuthorId));

        }

        private async Task VerifyAuthorExists(Guid currentAuthorId)
        {
            if (!await _authorService.Exists(currentAuthorId))
            {
                await _authorService.Add(currentAuthorId);
            }
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
            throw new NotImplementedException();
        }
    }
}