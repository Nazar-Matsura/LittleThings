using LittleThingsToDo.Application;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.Infrastructure;
using LittleThingsToDo.TelegramBot.Filters;
using LittleThingsToDo.TelegramBot.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LittleThingsToDo.TelegramBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddSingleton<IBotClient, BotClient>();
            services.AddScoped<ICurrentAuthorService, CurrentAuthorService>();
            services.AddTransient<ITempService, TempService>();

            services.Configure<BotConfiguration>(Configuration.GetSection("TelegramBot"));
            
            services
                .AddControllers(options => 
                    options.Filters.Add(typeof(CurrentAuthorSetterFilter)))
                .AddNewtonsoftJson();

            services.AddApplication();

            services.AddInfrastructure(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
