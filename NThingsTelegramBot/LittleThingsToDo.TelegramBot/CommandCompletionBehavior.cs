using System.Threading;
using System.Threading.Tasks;
using LittleThingsToDo.TelegramBot.Services;
using LittleThingsToDo.TelegramBot.Storage;
using MediatR;

namespace LittleThingsToDo.TelegramBot
{
    public class CommandCompletionBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly ICommandStorage _commandStorage;
        protected readonly ICurrentChatService _currentChatService;

        public CommandCompletionBehavior(ICommandStorage commandStorage,
            ICurrentChatService currentChatService)
        {
            _commandStorage = commandStorage;
            _currentChatService = currentChatService;
        }
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            await next.Invoke();

            if(request is IForceReplyCommand command)
                await _commandStorage.CompleteAsync(command, _currentChatService.CurrentChatId);

            return default;
        }
    }
}