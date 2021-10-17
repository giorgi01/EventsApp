using System;

namespace EventsApp.ArchiveWorker
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime PlannedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = false;
        public bool IsArchived { get; set; } = false;
        public string UserId { get; set; }
    }
}