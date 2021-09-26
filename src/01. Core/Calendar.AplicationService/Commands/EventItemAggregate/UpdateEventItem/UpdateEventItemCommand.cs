using Calendar.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;

namespace Calendar.AplicationService.Commands.EventItemAggregate.UpdateEventItem
{
    public class UpdateEventItemCommand : IRequest<UpdateEventItemDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string Location { get; set; }
        public string EventOrganizer { get; set; }
        public List<string> Members { get; set; }
    }
}
