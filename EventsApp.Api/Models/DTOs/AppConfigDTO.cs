using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsApp.Api.Models.DTOs
{
    public class AppConfigDTO
    {
        public string Setting { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
    }
}
