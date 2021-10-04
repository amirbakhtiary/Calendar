using Calendar.Core.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Calendar.Infrastructure.Data.Sql.Repository
{
    public interface IEventItemRepository : IRepository<EventItem>
    {
        Task<EventItem> GetEventItemByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
