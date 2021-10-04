using Calendar.Core.Domain.Entities;
using Calendar.Infrastructure.Data.Sql.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Calendar.AplicationService.Queries.EventItemAggregate.GetEventItemByOrganizer
{
    public class GetEventItemByOrganizerQueryHandler : IRequestHandler<GetEventItemByOrganizerQuery, List<EventItemDto>>
    {
        private readonly IEventItemRepository _eventItemRepository;

        public GetEventItemByOrganizerQueryHandler(IEventItemRepository eventItemRepository)
        {
            _eventItemRepository = eventItemRepository;
        }

        public async Task<List<EventItemDto>> Handle(GetEventItemByOrganizerQuery request, CancellationToken cancellationToken)
        {
            return await _eventItemRepository.GetAll()
                .AsNoTracking()
                .Where(o => o.EventOrganizer.Name == request.EventOrganizerName)
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
