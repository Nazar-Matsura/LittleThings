using Microsoft.EntityFrameworkCore;
using LittleThingsToDo.TelegramBot.Storage;

namespace LittleThingsToDo.TelegramBot.Storage
{
    internal class TelegramStorageDbContext : DbContext
    {
        public TelegramStorageDbContext(DbContextOptions<TelegramStorageDbContext> options)
            : base(options)
        {
        }

        public DbSet<ChatCommand> LastCommands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatCommand>().Property(c => c.Id).ValueGeneratedNever();

            base.OnModelCreating(modelBuilder);
        }
    }
}