using System;

namespace LittleThingsToDo.TelegramBot.Services
{
    public class CommandConstantsService : ICommandConstantsService
    {
        public string AddNewIdentifier => Guid.Empty.ToString();
    }
}
