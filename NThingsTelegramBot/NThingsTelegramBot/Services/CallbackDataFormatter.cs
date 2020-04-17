using System;

namespace LittleThingsToDo.TelegramBot.Services
{
    public class CallbackDataFormatter : ICallbackDataFormatter
    {
        public string AddNewIdentifier => Guid.Empty.ToString();
    }
}
