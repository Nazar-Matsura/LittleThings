using LittleThingsToDo.Application.Interfaces.Infrastructure;
using LittleThingsToDo.Infrastructure.Common;
using LittleThingsToDo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LittleThingsToDo.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<ILongToGuidConverter, LongToGuidConverter>();
        }
    }
}
