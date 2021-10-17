using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventsApp.Domain.POCO
{
    public class AppConfig
    {
        [Key]
        public string Setting { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
    }
}
