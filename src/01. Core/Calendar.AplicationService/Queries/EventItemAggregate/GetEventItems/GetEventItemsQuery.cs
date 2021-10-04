using MediatR;
using System.Collections.Generic;

namespace Calendar.AplicationService.Queries.EventItemAggregate.GetEventItems
{
    public class GetEventItemsQuery : IRequest<List<EventItemDto>>
    {
    }
}
