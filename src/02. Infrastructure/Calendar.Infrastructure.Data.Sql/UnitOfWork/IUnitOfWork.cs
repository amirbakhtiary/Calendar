using System.Threading;
using System.Threading.Tasks;

namespace Calendar.Infrastructure.Data.Sql.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
