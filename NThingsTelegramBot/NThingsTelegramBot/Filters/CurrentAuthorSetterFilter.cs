using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Infrastructure;
using LittleThingsToDo.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Telegram.Bot.Types;

namespace LittleThingsToDo.TelegramBot.Filters
{
    public class CurrentAuthorSetterFilter : IAsyncActionFilter
    {
        private readonly ICurrentAuthorService _currentAuthorService;
        private readonly ILongToGuidConverter _guidConverter;

        public CurrentAuthorSetterFilter(ICurrentAuthorService currentAuthorService, ILongToGuidConverter guidConverter)
        {
            _currentAuthorService = currentAuthorService;
            _guidConverter = guidConverter;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var update = (Update) context.ActionArguments["update"];
            _currentAuthorService.CurrentAuthorId = _guidConverter.Convert(update.Message.Chat.Id);

            await next();
        }
    }
}