using System;
using System.IO;
using LittleThingsToDo.Application.Exceptions;
using LittleThingsToDo.Application.Interfaces.Infrastructure;
using LittleThingsToDo.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Telegram.Bot.Types;

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
