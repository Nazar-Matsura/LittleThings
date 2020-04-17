using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Infrastructure;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.Domain.Entities;

namespace LittleThingsToDo.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _repository;
        private readonly ILittleThingService _littleThingService;
        private static readonly List<string> DefaultLittleThings = new List<string>{ "yoga", "meditation", "diary" };


        public AuthorService(IRepository<Author> repository,
            ILittleThingService littleThingService)
        {
            _repository = repository;
            _littleThingService = littleThingService;
        }

        public async Task Add(Guid id)
        {
            await _repository.Add(new Author(id));
            // We assume that at all times ICurrentAuthorService is initialized.
            await _littleThingService.AddList(DefaultLittleThings);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _repository.Any(a => a.Id == id);
        }
    }
}
