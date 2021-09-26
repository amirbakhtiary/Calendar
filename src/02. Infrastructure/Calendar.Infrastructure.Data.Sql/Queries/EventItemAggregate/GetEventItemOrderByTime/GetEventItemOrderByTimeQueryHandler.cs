using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Calendar.Infrastructure.Data.Sql.Queries.EventItemAggregate.GetEventItemOrderByTime
{
    public class GetEventItemOrderByTimeQueryHandler : IRequestHandler<GetEventItemOrderByTimeQuery, List<GetEventItemOrderByTimeDto>>
    {
        private readonly CalendarDBContext _context;

        public GetEventItemOrderByTimeQueryHandler(CalendarDBContext context)
        {
            _context = context;
        }

        public Task<List<GetEventItemOrderByTimeDto>> Handle(GetEventItemOrderByTimeQuery request, CancellationToken cancellationToken)
        {
            return _context.EventItems
                .OrderByDescending(e => e.EventTime)
                .AsNoTracking()
                .Select(o => new GetEventItemOrderByTimeDto
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
