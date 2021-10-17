using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace EventsApp.ArchiveWorker
{
    public class ApiClient
    {
        private readonly ILogger<ApiClient> _logger;
        private readonly string _url;

        public ApiClient(ILogger<ApiClient> logger)
        {
            _logger = logger;
        }

        public async Task<List<Event>> GetEventsAsync()
        {

            using var client = new HttpClient();
            try
            {
                var response = await client.GetAsync("http://localhost:54853/api/v1/events");

                string result = response.Content.ReadAsStringAsync().Result;

                var events = JsonConvert.DeserializeObject<List<Event>>(result);

                _logger.LogInformation(result);

                return events;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task ArchiveEventAsync(Event eventModel)
        {
            using var client = new HttpClient();
            try
            {
                eventModel.IsArchived = true;
                var response = await client.PutAsJsonAsync("http://localhost:54853/api/v1/events", eventModel);

                string result = response.Content.ReadAsStringAsync().Result;

                _logger.LogInformation(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}