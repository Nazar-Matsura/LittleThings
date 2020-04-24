using System.Threading.Tasks;

namespace LittleThingsToDo.TelegramBot.Storage
{
    public interface ICommandStorage
    {
        void AddOrUpdate(ChatCommand command);

        Task<ChatCommand> GetLastCommandOfChatAsync(long chatId);

        Task CompleteAsync(ChatCommand command);
    }
}