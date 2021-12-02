using System.Threading;
using System.Threading.Tasks;

namespace Examination.Domain.SeedWork
{
    public interface IRepositoryBase<T> where T : IAggregateRoot
    {
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
        void StartTransaction();
        Task CommitTransactionAsync(T entity, CancellationToken cancellationToken = default);
        Task AbortTransactionAsync(CancellationToken cancellationToken = default);
    }
}
