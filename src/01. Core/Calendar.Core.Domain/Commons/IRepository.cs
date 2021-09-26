using System;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Core.Domain.Commons
{
    public partial interface ICalendarRepository
    {
        Task<EventItem> GetByIdAsync(Guid id);
        Task<EventItem> AddItemAsync(EventItem entity);
        EventItem UpdateItem(EventItem entity);
        void DeleteItem(EventItem entity);
        IQueryable<EventItem> Table { get; }
        IQueryable<EventItem> TableNoTracking { get; }
    }
}
