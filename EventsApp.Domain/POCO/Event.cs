using EventsApp.Domain.POCO;
using System;
using System.ComponentModel.DataAnnotations;

namespace EventsApp.Domain
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public DateTime PlannedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = false;
        public bool IsArchived { get; set; } = false;
        [Required]
        public string UserId { get; set; }
        public ApiUser User { get; set; }
    }
}
