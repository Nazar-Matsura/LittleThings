using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NThingsTelegramBot.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace NThingsTelegramBot.Controllers
{
    [Route("api/update")]
    public class MessageController : Controller
    {
        private readonly ITempService _tempService;

        public MessageController(ITempService tempService)
        {
            _tempService = tempService;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            if (update.Type == UpdateType.Message)
            {
                await _tempService.SayHello(update.Message);
            }

            return Ok();
        }
    }
}