using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Calendar.Infrastructure.Data.Sql.Queries.EventItemAggregate.GetEventItemById
{
    public class GetEventItemByIdQueryHandler : IRequestHandler<GetEventItemByIdQuery, GetEventItemByIdDto>
    {
        private readonly CalendarDBContext _context;

        public GetEventItemByIdQueryHandler(CalendarDBContext context)
        {
            _context = context;
        }

        public Task<GetEventItemByIdDto> Handle(GetEventItemByIdQuery request, CancellationToken cancellationToken)
        {
            return _context.EventItems
                .Where(o => o.Id == request.Id)
                .AsNoTracking()
                .Select(o => new GetEventItemByIdDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Time = o.EventTime,
                    Location = o.Location.Name,
                    EventOrganizer = o.EventOrganizer.Name,
                    Members = o.Members.Select(m => m.Name).ToList(),
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
