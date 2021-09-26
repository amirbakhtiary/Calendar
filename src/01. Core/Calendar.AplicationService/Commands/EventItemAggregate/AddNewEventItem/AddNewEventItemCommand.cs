using Calendar.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;

namespace Calendar.AplicationService.Commands.EventItemAggregate.AddNewEventItem
{
    public class AddNewEventItemCommand : IRequest<AddNewEventItemDto>
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string Location { get; set; }
        public string EventOrganizer { get; set; }
        public List<string> Members { get; set; }
    }
}
