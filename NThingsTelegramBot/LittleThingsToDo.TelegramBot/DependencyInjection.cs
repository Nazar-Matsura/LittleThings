using System.Reflection;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LittleThingsToDo.TelegramBot
{
    public static class DependencyInjection
    {
        public static void AddTelegramBot(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BotConfiguration>(configuration.GetSection("TelegramBot"));

            services.AddSingleton<IBotClient, BotClient>();
            services.AddScoped<ICurrentAuthorService, CurrentAuthorService>();
            services.AddScoped<ICurrentChatService, CurrentChatService>();

            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
