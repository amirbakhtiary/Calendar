using Calendar.Infrastructure.Data.Sql.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Calendar.AplicationService.Queries.EventItemAggregate.GetEventItems
{
    public class GetEventItemsQueryHandler : IRequestHandler<GetEventItemsQuery, List<EventItemDto>>
    {
        private readonly IEventItemRepository _eventItemRepository;

        public GetEventItemsQueryHandler(IEventItemRepository eventItemRepository)
        {
            _eventItemRepository = eventItemRepository;
        }
        public Task<List<EventItemDto>> Handle(GetEventItemsQuery request, CancellationToken cancellationToken)
        {
            return _eventItemRepository.GetAll()
                .AsNoTracking()
                .Select(o => new EventItemDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Time = o.EventTime,
                    Location = o.Location.Name,
                    EventOrganizer = o.EventOrganizer.Name,
                    Members = o.Members.Select(m => m.Name).ToList(),
                })
                .ToListAsync(cancellationToken);
        }
    }
}
