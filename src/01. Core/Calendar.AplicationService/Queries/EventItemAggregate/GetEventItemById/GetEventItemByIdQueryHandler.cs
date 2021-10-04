using Calendar.Core.Domain.Entities;
using Calendar.Infrastructure.Data.Sql.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Calendar.AplicationService.Queries.EventItemAggregate.GetEventItemById
{
    public class GetEventItemByIdQueryHandler : IRequestHandler<GetEventItemByIdQuery, EventItem>
    {
        private readonly IEventItemRepository _eventItemRepository;

        public GetEventItemByIdQueryHandler(IEventItemRepository eventItemRepository)
        {
            _eventItemRepository = eventItemRepository;
        }

        public async Task<EventItem> Handle(GetEventItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await _eventItemRepository.GetEventItemByIdAsync(request.Id, cancellationToken);
        }
    }
}
