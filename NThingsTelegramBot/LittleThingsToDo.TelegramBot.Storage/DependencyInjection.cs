using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LittleThingsToDo.TelegramBot.Storage
{
    public static class DependencyInjection
    {
        public static void AddTelegramBotStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TelegramStorageDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TelegramBotStorage")));

            services.AddTransient<ICommandStorage, CommandStorage>();
        }
    }
}
