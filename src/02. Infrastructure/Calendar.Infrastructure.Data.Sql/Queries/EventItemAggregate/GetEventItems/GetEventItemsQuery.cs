using MediatR;
using System.Collections.Generic;

namespace Calendar.Infrastructure.Data.Sql.Queries.EventItemAggregate.GetEventItems
{
    public class GetEventItemsQuery : IRequest<List<GetEventItemsDto>>
    {
    }
}
