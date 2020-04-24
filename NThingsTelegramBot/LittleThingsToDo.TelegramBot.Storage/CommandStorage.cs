using System.Linq;
using System.Threading.Tasks;

namespace LittleThingsToDo.TelegramBot.Storage
{
    internal class CommandStorage : ICommandStorage
    {
        private readonly TelegramStorageDbContext _dbContext;

        public CommandStorage(TelegramStorageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddOrUpdate(ChatCommand command)
        {
            var commandExists = _dbContext.LastCommands.Any(c => c.Id == command.Id);
            if (commandExists)
            {
                _dbContext.Update(command);
            }
            else
            {
                _dbContext.Add(command);
            }

            _dbContext.SaveChanges();
        }

        public async Task<ChatCommand> GetLastCommandOfChatAsync(long chatId)
        {
            return await _dbContext.LastCommands.FindAsync(chatId);
        }

        public async Task CompleteAsync(ChatCommand command)
        {
            _dbContext.LastCommands.Remove(command);

            await _dbContext.SaveChangesAsync();
        }
    }
}