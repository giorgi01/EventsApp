using System;
using System.Net.Http;

namespace EventsApp.Services
{
    public class EventsAPI
    {
        public HttpClient Client;
        public EventsAPI(string uri)
        {
            Client = new HttpClient()
            {
                BaseAddress = new Uri(uri)
            };
        }
    }
}
