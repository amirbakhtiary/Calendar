using MediatR;
using System.Collections.Generic;

namespace Calendar.Infrastructure.Data.Sql.Queries.EventItemAggregate.GetEventItemByOrganizer
{
    public class GetEventItemByOrganizerQuery : IRequest<List<GetEventItemByOrganizerDto>>
    {
        public GetEventItemByOrganizerQuery(string eventOrganizerName)
        {
            EventOrganizerName = eventOrganizerName;
        }
        public string EventOrganizerName { get; }
    }
}
