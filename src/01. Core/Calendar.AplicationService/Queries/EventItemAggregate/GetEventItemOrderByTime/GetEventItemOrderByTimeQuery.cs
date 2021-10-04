using MediatR;
using System.Collections.Generic;

namespace Calendar.AplicationService.Queries.EventItemAggregate.GetEventItemOrderByTime
{
    public class GetEventItemOrderByTimeQuery : IRequest<List<EventItemDto>>
    {
    }
}
