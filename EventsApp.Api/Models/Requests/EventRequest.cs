using Microsoft.AspNetCore.Http;
using System;

namespace EventsApp.Api.Models.Requests
{
    public class EventRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime PlannedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsArchived { get; set; } = false;
    }
}
