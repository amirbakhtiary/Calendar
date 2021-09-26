using Calendar.Core.Domain.Commons;
using System.Threading;
using System.Threading.Tasks;

namespace Calendar.Infrastructure.Data.Sql.Commons
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CalendarDBContext _dbContext;
        public UnitOfWork(CalendarDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
