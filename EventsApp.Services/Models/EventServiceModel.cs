using EventsApp.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsApp.Services.Models
{
    public class EventServiceModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime PlannedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsArchived { get; set; } = false;
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public string Author { get; set; }
    }
}
