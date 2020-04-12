using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LittleThingsToDo.TelegramBot.Controllers
{
    [Route("api/update")]
    public class MessageController : Controller
    {
        private readonly ITempService _tempService;
        private readonly ICurrentAuthorService _currentAuthorService;


        public MessageController(ITempService tempService,
            ICurrentAuthorService currentAuthorService)
        {
            _tempService = tempService;
            _currentAuthorService = currentAuthorService;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            var test = _currentAuthorService.CurrentAuthorId;
            if (update.Type == UpdateType.Message)
            {
                await _tempService.SayHello(update.Message);
            }

            return Ok();
        }
    }
}