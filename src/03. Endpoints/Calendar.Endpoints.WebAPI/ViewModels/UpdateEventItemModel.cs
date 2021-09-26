using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Calendar.Endpoints.WebAPI.ViewModels
{
    public class UpdateEventItemModel
    {
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        [StringLength(120)]
        public string Location { get; set; }
        [Required]
        [StringLength(120)]
        public string EventOrganizer { get; set; }
        [Required]
        public List<string> Members { get; set; }
    }
}
