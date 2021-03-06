using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.AplicationService.Commands.EventItemAggregate.AddNewEventItem
{
    public class AddNewEventItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string Location { get; set; }
        public string EventOrganizer { get; set; }
        public List<string> Members { get; set; }
    }
}
