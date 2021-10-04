using Calendar.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Calendar.Infrastructure.Data.Sql.Repository
{
    public class EventItemRepository : Repository<EventItem>, IEventItemRepository
    {
        public EventItemRepository(CalendarContext calendarContext) : base(calendarContext)
        {
        }

        public async Task<EventItem> GetEventItemByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _calendarContext
                .EventItems
                .Include(e => e.EventOrganizer)
                .Include(e => e.Location)
                .Include(e => e.Members)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
