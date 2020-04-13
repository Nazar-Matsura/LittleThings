using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.Commands;
using LittleThingsToDo.TelegramBot.Commands.Interfaces;
using LittleThingsToDo.TelegramBot.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LittleThingsToDo.TelegramBot.Controllers
{
    [Route("api/update")]
    public class MessageController : Controller
    {
        private readonly ICommand _command;
        private readonly ICurrentAuthorService _currentAuthorService;


        public MessageController(ICommand command,
            ICurrentAuthorService currentAuthorService)
        {
            _command = command;
            _currentAuthorService = currentAuthorService;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            var test = _currentAuthorService.CurrentAuthorId;
            if (update.Type == UpdateType.Message)
            {
                await _command.SayHello(update.Message);
            }

            return Ok();
        }
    }
}