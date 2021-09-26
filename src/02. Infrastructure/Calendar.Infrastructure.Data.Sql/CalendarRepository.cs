using Calendar.Core.Domain;
using Calendar.Core.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Infrastructure.Data.Sql
{
    public class CalendarRepository : ICalendarRepository
    {
        private DbSet<EventItem> DbSet { get; }
        public CalendarRepository(CalendarDBContext calendarDBContext)
        {
            DbSet = calendarDBContext.Set<EventItem>();
        }
        public async Task<EventItem> GetByIdAsync(Guid id)
        {
            return await DbSet
                .Include(o => o.Members)
                .Include(o => o.Location)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<EventItem> AddItemAsync(EventItem entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }
        public EventItem UpdateItem(EventItem entity)
        {
            DbSet.Update(entity);
            return entity;
        }
        public void DeleteItem(EventItem entity)
        {
            DbSet.Remove(entity);
        }
        public IQueryable<EventItem> Table => DbSet;
        public IQueryable<EventItem> TableNoTracking => DbSet.AsNoTracking();
    }
}
