using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Calendar.Infrastructure.Data.Sql.Queries.EventItemAggregate.GetEventItems
{
    public class GetEventItemsQueryHandler : IRequestHandler<GetEventItemsQuery, List<GetEventItemsDto>>
    {
        private readonly CalendarDBContext _context;

        public GetEventItemsQueryHandler(CalendarDBContext context)
        {
            _context = context;
        }
        public Task<List<GetEventItemsDto>> Handle(GetEventItemsQuery request, CancellationToken cancellationToken)
        {
            return _context.EventItems
                .AsNoTracking()
                .Select(o => new GetEventItemsDto
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
