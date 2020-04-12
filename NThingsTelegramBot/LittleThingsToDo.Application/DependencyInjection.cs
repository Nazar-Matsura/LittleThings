using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LittleThingsToDo.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAuthorService, AuthorService>();
        }
    }
}
