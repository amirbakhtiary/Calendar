using System.Threading;
using System.Threading.Tasks;

namespace Calendar.Infrastructure.Data.Sql.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CalendarContext _calendarContext;
        public UnitOfWork(CalendarContext calendarContext)
        {
            _calendarContext = calendarContext;
        }
        public void Save()
        {
            _calendarContext.SaveChanges();
        }

        public async Task SaveAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await _calendarContext.SaveChangesAsync(cancellationToken);
        }
    }
}
