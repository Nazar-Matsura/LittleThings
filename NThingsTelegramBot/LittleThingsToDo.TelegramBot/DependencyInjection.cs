using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.Commands.Classes;
using LittleThingsToDo.TelegramBot.Commands.Interfaces;
using LittleThingsToDo.TelegramBot.LittleThing.AddLittleThing;
using LittleThingsToDo.TelegramBot.LittleThing.AddLittleThingMenu;
using LittleThingsToDo.TelegramBot.Services;
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
            services.AddScoped<ICallbackDataFormatter, CallbackDataFormatter>();
            services.AddTransient<IMenuCommand, MenuCommand>();
            services.AddTransient<IAddLittleThingMenuCommand, AddLittleThingMenuCommand>();
            services.AddTransient<IAddLittleThingsCommand, AddLittleThingsCommandHandler>();
        }
    }
}
