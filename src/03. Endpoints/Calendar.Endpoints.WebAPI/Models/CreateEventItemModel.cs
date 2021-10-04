using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Calendar.Endpoints.WebAPI.Models
{
    public class CreateEventItemModel
    {
        [Required]
        [StringLength(250, ErrorMessage = "The event name must between 2 and 250 character", MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        [StringLength(120, ErrorMessage = "The location must between 2 and 120 character", MinimumLength = 2)]
        public string Location { get; set; }
        [Required]
        [StringLength(120, ErrorMessage = "The eventOrganizer must between 2 and 120 character", MinimumLength = 2)]
        public string EventOrganizer { get; set; }
        [Required]
        public List<string> Members { get; set; }
    }
}
