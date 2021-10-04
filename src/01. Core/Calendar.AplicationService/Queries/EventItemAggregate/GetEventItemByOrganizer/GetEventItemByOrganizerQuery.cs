using Calendar.Core.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Calendar.AplicationService.Queries.EventItemAggregate.GetEventItemByOrganizer
{
    public class GetEventItemByOrganizerQuery : IRequest<List<EventItemDto>>
    {
        public GetEventItemByOrganizerQuery(string eventOrganizerName)
        {
            EventOrganizerName = eventOrganizerName;
        }
        public string EventOrganizerName { get; }
    }
}
