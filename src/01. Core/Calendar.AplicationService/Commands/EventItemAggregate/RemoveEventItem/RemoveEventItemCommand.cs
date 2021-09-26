using MediatR;
using System;

namespace Calendar.AplicationService.Commands.EventItemAggregate.RemoveEventItem
{
    public class RemoveEventItemCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
