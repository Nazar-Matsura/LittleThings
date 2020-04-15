using System;
using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Infrastructure;
using LittleThingsToDo.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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
            var chatId = GetMessage(update).Chat.Id;
            _currentAuthorService.CurrentAuthorId = _guidConverter.Convert(chatId);

            await next();
        }

        private Message GetMessage(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    return update.Message;
                case UpdateType.CallbackQuery:
                    return update.CallbackQuery.Message;
                case UpdateType.EditedMessage:
                    return update.EditedMessage;
                case UpdateType.ChannelPost:
                    return update.ChannelPost;
                case UpdateType.EditedChannelPost:
                    return update.EditedChannelPost;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}