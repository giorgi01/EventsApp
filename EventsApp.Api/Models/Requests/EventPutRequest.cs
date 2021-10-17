using EventsApp.Domain.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventsApp.Api.Models.Requests
{
    public class EventPutRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime PlannedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsArchived { get; set; } = false;
    }
}
