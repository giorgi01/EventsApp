using System.Collections.Generic;

namespace EventsApp.Services.Models
{
    public class UserServiceModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<EventServiceModel> Events { get; set; }
    }
}