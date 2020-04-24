using MediatR;

namespace LittleThingsToDo.TelegramBot.Services
{
    public interface ICallbackDataFormatter
    {
        string ToString(IRequest request);
        IRequest FromString(string request);
    }
}