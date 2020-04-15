using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.Commands;
using LittleThingsToDo.TelegramBot.Commands.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LittleThingsToDo.TelegramBot.Controllers
{
    [Route("api/update")]
    public class MessageController : Controller
    {
        private readonly IMenuCommand _command;
        private readonly IAddLittleThingMenuCommand _addCommand;
        private readonly ICurrentAuthorService _currentAuthorService;
        
        public MessageController(IMenuCommand command,
            ICurrentAuthorService currentAuthorService,
            IAddLittleThingMenuCommand addCommand)
        {
            _command = command;
            _currentAuthorService = currentAuthorService;
            _addCommand = addCommand;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            var test = _currentAuthorService.CurrentAuthorId;
            await _addCommand.Handle(update);

            return Ok();
        }
    }
}