using System;
using System.Collections.Generic;

namespace Calendar.Core.Domain.Entities
{
    public static class EventItemCommand
    {
        public class AddNewEventItem
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public DateTime EventTime { get; set; }
            public string Location { get; set; }
            public string EventOrganizer { get; set; }
            public List<string> Members { get; set; }
        }

        public class UpdateEventItem
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public DateTime EventTime { get; set; }
            public string Location { get; set; }
            public string EventOrganizer { get; set; }
            public List<string> Members { get; set; }
        }
    }
}
