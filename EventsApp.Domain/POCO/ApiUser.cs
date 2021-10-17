using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EventsApp.Domain.POCO
{
    // ApiUser -> EvetsApp.Api
    // ApplicationUser -> EventsApp.MVCs
    public class ApiUser
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<Event> Events { get; set; }
    }
}
