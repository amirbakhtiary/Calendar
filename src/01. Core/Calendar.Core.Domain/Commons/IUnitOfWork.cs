using System.Threading;
using System.Threading.Tasks;

namespace Calendar.Core.Domain.Commons
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
