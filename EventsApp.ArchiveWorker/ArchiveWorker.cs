using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventsApp.ArchiveWorker
{
    public class ArchiveWorker : BackgroundService
    {
        private readonly ILogger<ArchiveWorker> _logger;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private ApiClient _client;

        private string Schedule => "*/30 * * * *"; // At every 30th minute

        public ArchiveWorker(ILogger<ArchiveWorker> logger, ApiClient client)
        {
            _logger = logger;
            _schedule = CrontabSchedule.Parse(Schedule);
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            _client = client;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private async void Process()
        {
            var events = await _client.GetEventsAsync();
            foreach (Event eventModel in events)
            {
                if (eventModel.PlannedAt < DateTime.Now) await _client.ArchiveEventAsync(eventModel);
            }
        }
    }
}
