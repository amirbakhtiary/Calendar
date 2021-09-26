using MediatR;
using System;

namespace Calendar.Infrastructure.Data.Sql.Queries.EventItemAggregate.GetEventItemById
{
    public class GetEventItemByIdQuery : IRequest<GetEventItemByIdDto>
    {
        public GetEventItemByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
