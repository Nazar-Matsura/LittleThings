using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.TelegramBot.Services;
using MediatR;

namespace LittleThingsToDo.TelegramBot.LittleThing.GetEntriesForToday
{
    public class GetEntriesForTodayCommandHandler : CommandHandlerBase, IRequestHandler<GetEntriesForTodayCommand>
    {
        protected readonly ILittleThingService _littleThingService;
        public GetEntriesForTodayCommandHandler(IBotClient botClient,
            ICurrentChatService currentChatService,
            ILittleThingService littleThingService)
            : base(botClient, currentChatService)
        {
            _littleThingService = littleThingService;
        }

        public async Task<Unit> Handle(GetEntriesForTodayCommand request, CancellationToken cancellationToken)
        {
            var todayEntries = await _littleThingService.GetEntriesForToday();
            var todayEntriesWithCount = todayEntries
                .GroupBy(e => e.LittleThingId)
                .Select(g => new {name = g.First().LittleThing.Name, count = g.Count()})
                .ToList();

            var text = todayEntriesWithCount.Any() ? BuildList(todayEntriesWithCount) : "nothing for today";

            await ReplyText(text);
            return default;
        }

        private string BuildList(IEnumerable<dynamic> todayEntriesWithCount)
        {
            var result = new StringBuilder();
            foreach (var entry in todayEntriesWithCount)
            {
                result.Append("* ");
                result.Append(entry.name);
                if (entry.count > 1)
                    result.Append($" - {entry.count}");
                result.AppendLine();
            }

            return result.ToString();
        }
    }
}
