using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Calendar.Infrastructure.Data.Sql.Queries.EventItemAggregate.GetEventItemByOrganizer
{
    public class GetEventItemByOrganizerQueryHandler : IRequestHandler<GetEventItemByOrganizerQuery, List<GetEventItemByOrganizerDto>>
    {
        private readonly CalendarDBContext _context;

        public GetEventItemByOrganizerQueryHandler(CalendarDBContext context)
        {
            _context = context;
        }

        public Task<List<GetEventItemByOrganizerDto>> Handle(GetEventItemByOrganizerQuery request, CancellationToken cancellationToken)
        {
            return _context.EventItems
                .Where(o => o.EventOrganizer.Name == request.EventOrganizerName)
                .AsNoTracking()
                .Select(o => new GetEventItemByOrganizerDto
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
