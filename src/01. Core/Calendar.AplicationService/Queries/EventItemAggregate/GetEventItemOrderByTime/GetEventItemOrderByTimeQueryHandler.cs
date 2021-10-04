using Calendar.Infrastructure.Data.Sql.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Calendar.AplicationService.Queries.EventItemAggregate.GetEventItemOrderByTime
{
    public class GetEventItemOrderByTimeQueryHandler : IRequestHandler<GetEventItemOrderByTimeQuery, List<EventItemDto>>
    {
        private readonly IEventItemRepository _eventItemRepository;

        public GetEventItemOrderByTimeQueryHandler(IEventItemRepository eventItemRepository)
        {
            _eventItemRepository = eventItemRepository;
        }

        public Task<List<EventItemDto>> Handle(GetEventItemOrderByTimeQuery request, CancellationToken cancellationToken)
        {
            return _eventItemRepository.GetAll()
                .OrderByDescending(e => e.EventTime)
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
