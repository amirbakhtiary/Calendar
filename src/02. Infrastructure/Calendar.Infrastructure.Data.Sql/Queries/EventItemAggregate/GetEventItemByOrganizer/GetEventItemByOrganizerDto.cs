using System;
using System.Collections.Generic;

namespace Calendar.Infrastructure.Data.Sql.Queries.EventItemAggregate.GetEventItemByOrganizer
{
    public class GetEventItemByOrganizerDto
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string Location { get; set; }
        public string EventOrganizer { get; set; }
        public List<string> Members { get; set; }
    }
}
