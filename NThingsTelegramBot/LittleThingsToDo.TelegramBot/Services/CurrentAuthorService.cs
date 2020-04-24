using System;
using LittleThingsToDo.Application.Exceptions;
using LittleThingsToDo.Application.Interfaces.Services;

namespace LittleThingsToDo.TelegramBot.Services
{
    public class CurrentAuthorService : ICurrentAuthorService
    {
        private Guid? _currentAuthorId;

        public Guid CurrentAuthorId
        {
            get => _currentAuthorId ?? throw new NoCurrentAuthorException();
            set => _currentAuthorId = value;
        }
    }
}
