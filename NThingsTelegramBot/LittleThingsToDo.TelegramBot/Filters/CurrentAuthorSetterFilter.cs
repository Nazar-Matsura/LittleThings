using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Infrastructure;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Telegram.Bot.Types;

namespace LittleThingsToDo.TelegramBot.Filters
{
    public class CurrentAuthorSetterFilter : IAsyncActionFilter
    {
        private readonly ICurrentAuthorService _currentAuthorService;
        private readonly IAuthorService _authorService;
        private readonly ICurrentChatService _currentChatService;
        private readonly ILongToGuidConverter _guidConverter;

        public CurrentAuthorSetterFilter(
            ICurrentAuthorService currentAuthorService,
            ILongToGuidConverter guidConverter,
            ICurrentChatService currentChatService,
            IAuthorService authorService)
        {
            _currentAuthorService = currentAuthorService;
            _guidConverter = guidConverter;
            _currentChatService = currentChatService;
            _authorService = authorService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var update = (Update) context.ActionArguments["update"];
            _currentChatService.SetFromUpdate(update);
            var currentAuthorId = _guidConverter.Convert(_currentChatService.CurrentChatId.Identifier);
            
            _currentAuthorService.CurrentAuthorId = currentAuthorId; // For now, be careful with order. Whole application assumes that this value initialized.

            if (!await _authorService.Exists(currentAuthorId))
            {
                await _authorService.Add(currentAuthorId);
            }

            await next();
        }
    }
}