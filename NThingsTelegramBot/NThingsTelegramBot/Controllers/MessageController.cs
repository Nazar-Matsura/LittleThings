using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace LittleThingsToDo.TelegramBot.Controllers
{
    [Route("api/update")]
    public class MessageController : Controller
    {
        private readonly ICurrentAuthorService _currentAuthorService; 
        private readonly ICommandResolver _commandResolver;
        private readonly IMediator _mediator;

        public MessageController(ICurrentAuthorService currentAuthorService,
            IAuthorService authorService,
            ICommandResolver commandResolver,
            IMediator mediator)
        {
            _currentAuthorService = currentAuthorService;
            _commandResolver = commandResolver;
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            var request = _commandResolver.Resolve(update);
            await _mediator.Send(request);

            return Ok();
        }
    }
}