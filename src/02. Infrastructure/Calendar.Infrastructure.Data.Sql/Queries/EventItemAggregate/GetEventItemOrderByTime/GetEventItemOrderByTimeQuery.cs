using MediatR;
using System.Collections.Generic;

namespace Calendar.Infrastructure.Data.Sql.Queries.EventItemAggregate.GetEventItemOrderByTime
{
    public class GetEventItemOrderByTimeQuery : IRequest<List<GetEventItemOrderByTimeDto>>
    {
    }
}
