using Calendar.Core.Domain.Entities;
using MediatR;
using System;

namespace Calendar.AplicationService.Queries.EventItemAggregate.GetEventItemById
{
    public class GetEventItemByIdQuery : IRequest<EventItem>
    {
        public GetEventItemByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
